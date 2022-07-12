using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,  IPointerDownHandler
{
    private bool _clicked = false;
    private bool _highlighted = false;
    private Vector3 _originalScale;

    private void Start()
    {
        RectTransform trans = GetComponent<RectTransform>();
        _originalScale = trans.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _highlighted = true;
        
        if (_clicked) return;

        Vector3 scale = _originalScale + Vector3.one * 0.05f;
        Vector3 rotation = Vector3.zero;
        float time = 0.5f;
        LeanTweenType rotateEaseType = LeanTweenType.easeInOutSine;
        LeanTweenType scaleEaseType = LeanTweenType.easeInOutSine;
        LeanTweenType loopType = LeanTweenType.pingPong;
        
        AnimateTween(scale, time, scaleEaseType, loopType, rotation, time, rotateEaseType, loopType);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        _highlighted = false;
        
        if (_clicked) return;

        Vector3 scale = _originalScale;
        Vector3 rotation = Vector3.zero;
        float time = 0.25f;
        LeanTweenType rotateEaseType = LeanTweenType.easeInOutSine;
        LeanTweenType scaleEaseType = LeanTweenType.easeInOutSine;
        LeanTweenType loopType = LeanTweenType.once;
        
        AnimateTween(scale, time, scaleEaseType, loopType, rotation, time, rotateEaseType, loopType);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_clicked) return;
        
        Vector3 scale = _originalScale - Vector3.one * 0.05f;
        Vector3 rotation = new Vector3(0, 0, 10f);
        float time = 0.25f;
        LeanTweenType rotateEaseType = LeanTweenType.easeInOutSine;
        LeanTweenType scaleEaseType = LeanTweenType.easeInOutSine;
        LeanTweenType rotateLoopType = LeanTweenType.pingPong;
        LeanTweenType scaleLoopType = LeanTweenType.once;

        LeanTween.scale(gameObject, scale, time).setEase(scaleEaseType).setLoopType(scaleLoopType);
        LeanTween.rotateLocal(gameObject, -rotation, time).setEase(rotateEaseType)
            .setOnComplete(() => AnimateTween(scale, time, scaleEaseType, scaleLoopType, rotation, time, rotateEaseType, rotateLoopType));
    }
    
    public void OnClick()
    {
        _clicked = true;
        LeanTween.cancel(gameObject);
        LeanTween.rotate(gameObject, Vector3.zero, 0.2f).setEaseInOutSine();
        LeanTween.scale(gameObject, _originalScale - Vector3.one * 0.1f, 0.15f).setEaseInOutSine()
            .setOnComplete(() => LeanTween.scale(gameObject, _originalScale + Vector3.one * 0.1f, 0.1f).setEaseInOutSine()
                .setOnComplete(() => LeanTween.scale(gameObject, _originalScale, 0.1f).setEaseInOutSine()
                    .setOnComplete(ClickComplete)));
    }

    private void ClickComplete()
    {
        _clicked = false;
        if (!_highlighted) return;
        
        OnPointerEnter(new PointerEventData(EventSystem.current));
    }
    
    /// <summary>
    /// Animates this.gameObject using LeanTween.
    /// </summary>
    /// <param name="scale">Scale to which to tween to.</param>
    /// <param name="scaleTime">Time in seconds to tween to given scale.</param>
    /// <param name="scaleEaseType">Type of ease used on the tween.</param>
    /// <param name="scaleLoopType">Type of loop. Set to LeanTweenType.once if no loop is desired.</param>
    /// <param name="rotation">Rotation to which to tween to.</param>
    /// <param name="rotateTime">Time in seconds to tween to given rotation.</param>
    /// <param name="rotateEaseType">Type of ease used on the tween.</param>
    /// <param name="rotateLoopType">Type of loop. Set to LeanTweenType.once if no loop is desired.</param>
    private void AnimateTween(Vector3 scale, float scaleTime, LeanTweenType scaleEaseType, LeanTweenType scaleLoopType, Vector3 rotation, float rotateTime, LeanTweenType rotateEaseType, LeanTweenType rotateLoopType)
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, scale, scaleTime).setEase(scaleEaseType).setLoopType(scaleLoopType);
        LeanTween.rotateLocal(gameObject, rotation, rotateTime).setEase(rotateEaseType).setLoopType(rotateLoopType);
    }
}
