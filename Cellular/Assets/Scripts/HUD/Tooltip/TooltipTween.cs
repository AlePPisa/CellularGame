using System;
using UnityEngine;

namespace HUD.Tooltip
{
    public class TooltipTween : MonoBehaviour
    {
        public float fadeInTime;
        public float fadeOutTime;

        private void Awake()
        {
            LeanTween.alpha(gameObject, 0f, 0f);
        }

        /// <summary>
        /// Animates the alpha of the tooltip in order to display it.
        /// </summary>
        public void AnimateShow()
        {
            LeanTween.alpha(gameObject, 1f, fadeInTime).setEaseInOutSine();
        }

        /// <summary>
        /// Animates the alpha of the tooltip in order to hide it.
        /// </summary>
        public void AnimateHide()
        {
            LeanTween.alpha(gameObject, 0f, fadeOutTime).setEaseInOutSine();
        }
    }
}