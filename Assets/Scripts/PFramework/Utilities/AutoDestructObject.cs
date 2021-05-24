using DG.Tweening;
using UnityEngine;

namespace PFramework
{
    public class AutoDestructObject : CacheMonoBehaviour
    {
        [Header("Config")]
        [SerializeField] float _delay = 0f;
        [SerializeField] bool _deactiveOnly = false;

        Tween _tween;

        #region MonoBehaviour

        void OnEnable()
        {
            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(_delay, Destruct);
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        #endregion

        #region Public

        public void ResetDestruct()
        {
            _tween?.Restart();
        }

        public void Construct(float delay)
        {
            _delay = delay;
            OnEnable();
        }

        #endregion

        void Destruct()
        {
            if (_deactiveOnly)
                CacheGameObject.SetActive(false);
            else
                Destroy(CacheGameObject);
        }
    }
}