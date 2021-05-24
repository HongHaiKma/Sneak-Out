using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    public class FxRingNoise : CacheMonoBehaviour
    {
        [Header("Config")]
        [SerializeField] float _startScale = 1f;
        [SerializeField] float _endScale = 20f;
        [SerializeField] Ease _ease = Ease.OutQuint;
        [SerializeField] float _scaleDuration = 1f;
        [SerializeField] float _fadeDuration = 0.2f;
        [SerializeField] float _fadeDelay = 0.8f;

        Sequence _sequence;

        #region MonoBehaviour

        void Awake()
        {
            SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            _sequence = DOTween.Sequence();

            CacheTransform.SetScaleXZ(_startScale, _startScale);

            _sequence.Append(CacheTransform.DOScale(new Vector3(_endScale, 1f, _endScale), _scaleDuration).SetEase(_ease));
            _sequence.Insert(_fadeDelay, spriteRenderer.DOFade(0f, _fadeDuration));

            _sequence.Play();
        }

        void OnDestroy()
        {
            _sequence?.Kill();
        }

        #endregion
    }
}