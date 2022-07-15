using System;
using System.Collections;
using System.Collections.Generic;
using HUD.Tooltip;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;
    public Tooltip tooltip;
    public TooltipTween tooltipTween;

    public void Awake()
    {
        current = this;
    }

    /// <summary>
    /// Sets tooltips content and header, then displays it.
    /// </summary>
    /// <param name="content">Tooltip's content.</param>
    /// <param name="header">Tooltip's header.</param>
    public static void Show(string content, string header = "")
    {
        current.tooltip.SetText(content, header);
        current.tooltip.gameObject.SetActive(true);
    }
    
    /// <summary>
    /// Hides the tooltip. 
    /// </summary>
    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }
}
