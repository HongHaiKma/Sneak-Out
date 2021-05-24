using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace PFramework
{
    public class SpriteRendererAnimated : CacheMonoBehaviour
    {
        [Header("Config")]
        [SerializeField] Sprite[] _sprFrames;
        [Min(1)]
        [SerializeField] float _delayBetweenFrames = 0.1f;
        [SerializeField] bool _autoPlay = true;

        [SerializeField] UnityEvent _onCycleFinished;

        SpriteRenderer _spriteRenderer;

        int _frameIndex = 0;

        Tween _tween;

        #region MonoBehaviour

        void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        void Start()
        {
            if (_autoPlay)
                NextFrame();
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        void OnEnable()
        {
            _tween?.Play();
        }

        void OnDisable()
        {
            _tween?.Pause();
        }

        #endregion

        public void Destroy()
        {
            Object.Destroy(gameObject);
        }

        void NextFrame()
        {
            bool cycleFinished = _frameIndex >= _sprFrames.Length;

            if (cycleFinished)
                _frameIndex = 0;

            _spriteRenderer.sprite = _sprFrames[_frameIndex];

            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(_delayBetweenFrames, NextFrame, false);

            _frameIndex++;

            if(cycleFinished)
                _onCycleFinished?.Invoke();
        }
    }
}