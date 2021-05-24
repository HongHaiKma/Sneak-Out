using DG.Tweening;
using UnityEngine;

namespace PFramework
{
    public class PopupTransitionFade : PopupTransition
    {
        [Header("Config")]
        [Range(0.1f, 1f)]
        [SerializeField] float _fadeDurationRatio = 1f;

        public override Tween ConstructTransition(PopupBehaviour popup)
        {
            CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

            // Set target start scale
            canvasGroup.alpha = 0f;

            return canvasGroup.DOFade(1f, popup.OpenDuration * _fadeDurationRatio);
        }
    }
}