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
    
    /// <summary>
    /// Activates delayed call on pointer enter in order to display tooltip after <see cref="delayTime"/> seconds.
    /// </summary>
    /// <param name="eventData">Current data regarding the event system.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = LeanTween.delayedCall(delayTime, () =>
        {
            TooltipSystem.Show(content, header);
        });
    }

    /// <summary>
    /// Cancels delayed call on pointer exit and hides tooltip.
    /// </summary>
    /// <param name="eventData">Current data regarding the event system.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }

    /// <summary>
    /// Increases delay time to show tooltip after <see cref="increasedDelayTime"/> seconds.
    /// </summary>
    public void IncreaseDelayTime()
    {
        delayTime = increasedDelayTime;
    }
}
