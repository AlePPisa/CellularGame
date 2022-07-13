using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellTweener : MonoBehaviour, IPointerClickHandler
{
    public float scaleTime;
    
    private bool _isAlive = false;
    private Vector3 _originalScale;
    private GameObject _cell;

    private void Start()
    {
        _cell = transform.GetChild(0).gameObject;
        _originalScale = _cell.transform.localScale;
        InstantHide();
    }

    public void AnimateBirth()
    {
        LeanTween.cancel(_cell);
        float correctionAngle = 360 - (_cell.transform.rotation.eulerAngles.z % 360);
        LeanTween.rotateAroundLocal(_cell, Vector3.forward, 360 + correctionAngle, scaleTime).setEaseInOutSine();
        LeanTween.scale(_cell, _originalScale, scaleTime).setEaseInOutSine();
    }
    
    public void AnimateDeath()
    {
        LeanTween.cancel(_cell);
        LeanTween.rotateLocal(_cell, Vector3.zero, scaleTime).setEaseInOutSine();
        LeanTween.scale(_cell, Vector3.zero, scaleTime).setEaseInOutSine();
    }

    private void InstantHide()
    {
        LeanTween.scale(_cell, Vector3.zero, 0f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isAlive)
        {
            AnimateDeath();
        }
        else
        {
            AnimateBirth();
        }

        _isAlive = !_isAlive;
    }
}
