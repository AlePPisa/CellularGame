using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    private Animator _animatorButtonToggle;
    private bool _state = false;

    private void Start()
    {
        _animatorButtonToggle = GetComponentInChildren<Animator>();
    }

    private void SetOn()
    {
        _animatorButtonToggle.SetTrigger("On");
        _state = true;
    }

    private void SetOff()
    {
        _animatorButtonToggle.SetTrigger("Off");
        _state = false;
    }

    public void Toggle()
    {
        if (_state)
        {
            SetOff();
        }
        else
        {
            SetOn();
        }
    }
    
}
