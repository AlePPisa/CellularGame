using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("Highlight Settings")] 
    [Tooltip("Percentage at which the sprite will grow and shrink")]
    public float highlightScale = 0.05f;
    public float highlightAnimationTime = 0.5f;
    
    [Header("Hold Settings")]
    [Tooltip("Percentage at which the sprite will grow and shrink")]
    public float holdScaleFactor = 0.05f;
    public Vector3 holdRotationInDegrees = new Vector3(0, 0, 10f);
    public float holdAnimationTime = 0.25f;
    
   
    [Header("Pointer Exit Settings")]
    public float exitAnimationTime = 0.25f;

   
    [Header("On Click Settings")] 
    public float timeToZeroRotation = 0.2f;
    [Tooltip("Percentage at which the sprite will grow and shrink")]
    public float clickScale = 0.1f;
    public float shrinkTime = 0.15f;
    public float growTimeAfterShrink = 0.1f;
    public float reachNormalSizeTime = 0.1f;
    
    private bool _clicked = false;
    private bool _highlighted = false;
    private Vector3 _originalScale;

    private void Start()
    {
        RectTransform trans = GetComponent<RectTransform>();
        _originalScale = trans.localScale;
    }

    /// <summary>
    /// Responsible for animating button when on pointer enter.
    /// </summary>
    /// <param name="eventData">Data regarding the current event system</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _highlighted = true;
        
        if (_clicked) return;

        Vector3 scale = _originalScale + Vector3.one * highlightScale;
        Vector3 rotation = Vector3.zero;
        float time = highlightAnimationTime;
        LeanTweenType rotateEaseType = LeanTweenType.easeInOutSine;
        LeanTweenType scaleEaseType = LeanTweenType.easeInOutSine;
        LeanTweenType loopType = LeanTweenType.pingPong;
        
        AnimateTween(scale, time, scaleEaseType, loopType, rotation, time, rotateEaseType, loopType);
    }
    
    /// <summary>
    /// Responsible for animating button when on pointer exit.
    /// </summary>
    /// <param name="eventData">Data regarding the current event system</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        _highlighted = false;
        
        if (_clicked) return;

        Vector3 scale = _originalScale;
        Vector3 rotation = Vector3.zero;
        float time = exitAnimationTime;
        LeanTweenType rotateEaseType = LeanTweenType.easeInOutSine;
        LeanTweenType scaleEaseType = LeanTweenType.easeInOutSine;
        LeanTweenType loopType = LeanTweenType.once;
        
        AnimateTween(scale, time, scaleEaseType, loopType, rotation, time, rotateEaseType, loopType);
    }
    
    /// <summary>
    /// Responsible for animating button when on pointer down.
    /// </summary>
    /// <param name="eventData">Data regarding the current event system</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_clicked) return;
        
        Vector3 scale = _originalScale - Vector3.one * holdScaleFactor;
        Vector3 rotation = holdRotationInDegrees;
        float time = holdAnimationTime;
        LeanTweenType rotateEaseType = LeanTweenType.easeInOutSine;
        LeanTweenType scaleEaseType = LeanTweenType.easeInOutSine;
        LeanTweenType rotateLoopType = LeanTweenType.pingPong;
        LeanTweenType scaleLoopType = LeanTweenType.once;

        LeanTween.scale(gameObject, scale, time).setEase(scaleEaseType).setLoopType(scaleLoopType);
        LeanTween.rotateLocal(gameObject, -rotation, time).setEase(rotateEaseType)
            .setOnComplete(() => AnimateTween(scale, time, scaleEaseType, scaleLoopType, rotation, time, rotateEaseType, rotateLoopType));
    }
    
    /// <summary>
    /// Responsible for animating button when on click.
    /// </summary>
    public void OnClick()
    {
        _clicked = true;
        LeanTween.cancel(gameObject);
        LeanTween.rotate(gameObject, Vector3.zero, timeToZeroRotation).setEaseInOutSine(); //zero rotation
        LeanTween.scale(gameObject, _originalScale - Vector3.one * clickScale, shrinkTime).setEaseInOutSine() // shrink to smaller scale
            .setOnComplete(() => LeanTween.scale(gameObject, _originalScale + Vector3.one * clickScale, growTimeAfterShrink).setEaseInOutSine() // grow to bigger scale
                .setOnComplete(() => LeanTween.scale(gameObject, _originalScale, reachNormalSizeTime).setEaseInOutSine() // resize to original size
                    .setOnComplete(ClickComplete))); // highlight again in case its being highlighted
    }

    /// <summary>
    /// Resets the animation to the proper state when click is complete.
    /// </summary>
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
