using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PFramework
{
    public class AutoSequence : CacheMonoBehaviour
    {
        [Header("Config")]
        [ListDrawerSettings(ShowIndexLabels = true)]
        [SerializeReference] List<AutoStep> _steps = new List<AutoStep>();
        [SerializeField] int _loopTime;
        [SerializeField] LoopType _loopType;
        [SerializeField] UpdateType _updateType;
        [SerializeField] bool _ignoreTimeScale = false;
        [SerializeField] bool _autoKill = true;
        [SerializeField] bool _playOnAwake;

        Sequence _sequence;
        RectTransform _rectTransform;
        Graphic _graphic;

        public Sequence Sequence => _sequence == null ? InitSequence() : _sequence;

        public RectTransform CacheRectTransform
        {
            get
            {
                if (_rectTransform == null)
                    _rectTransform = GetComponent<RectTransform>();
                return _rectTransform;
            }
        }

        public Graphic CacheGraphic
        {
            get
            {
                if (_graphic == null)
                    _graphic = GetComponent<Graphic>();
                return _graphic;
            }
        }

        #region MonoBehaviour

        void Awake()
        {
            if (_playOnAwake)
                Sequence.Play();
        }

        void OnDestroy()
        {
            _sequence?.Kill();
        }

        #endregion

        Sequence InitSequence()
        {
            if (_sequence != null)
                return _sequence;

            _sequence = DOTween.Sequence();

            for (int i = 0; i < _steps.Count; i++)
            {
                AutoStep step = _steps[i];

                step.Construct(this, _sequence);
            }

            _sequence.SetLoops(_loopTime, _loopType)
                .SetAutoKill(_autoKill)
                .SetUpdate(_updateType, _ignoreTimeScale)
                .Pause();

            return _sequence;
        }
    }
}