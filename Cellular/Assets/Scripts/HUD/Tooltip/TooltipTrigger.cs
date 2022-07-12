using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Tooltip("Time, in seconds, before tooltip is shown.")]
    public float delayTime = 2f;

    [Tooltip("Time, in seconds, before tooltip is shown after having clicked the button.")]
    public float increasedDelayTime = 4f;
    public string header;
   
    [Multiline]
    public string content;

    private static LTDescr delay;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = LeanTween.delayedCall(delayTime, () =>
        {
            TooltipSystem.Show(content, header);
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }

    public void IncreaseDelayTime()
    {
        delayTime = increasedDelayTime;
    }
}
