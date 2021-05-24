using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    public class PlayerNoise : CacheMonoBehaviour
    {
        static readonly int ProID_EmisiveColor = Animator.StringToHash("_EmisColor");

        [Header("Reference")]
        [SerializeField] Transform _tfRing;
        [SerializeField] SpriteRenderer _sprRing;
        [SerializeField] SpriteRenderer _sprCursor;
        [SerializeField] ParticleSystemRenderer _psrRing;

        [Header("Config")]
        [SerializeField] Vector2 _radiusRange;
        [SerializeField] float _radiusExtend;
        [SerializeField] float _fadeInDuration;
        [SerializeField] float _fadeStopDuration;
        [SerializeField] float _fadeOutDuration;

        float _radius;
        Sequence _sequence;
        Material _matRing;

        #region MonoBehaviour

        void Awake()
        {
            GetComponentInParent<CharacterMovement>().OnSpeedChanged += CharacterMovement_OnSpeedChanged;

            CharacterMovement_OnSpeedChanged(0f);

            _matRing = new Material(_psrRing.sharedMaterial);
            _psrRing.sharedMaterial = _matRing;

            InitSquence();
        }

        void Start()
        {
            Messenger.AddListener(GameEvent.Player_Detected, GameEvent_PlayerDetected);
        }

        void OnDestroy()
        {
            Messenger.RemoveListener(GameEvent.Player_Detected, GameEvent_PlayerDetected);

            _sequence?.Kill();
        }

        void Update()
        {
            if (_radius > 0f)
                Messenger.Broadcast(GameEvent.Player_Noise, CacheTransform.position, _radius);
        }

        #endregion

        void InitSquence()
        {
            _sequence = DOTween.Sequence();

            _sequence.Append(_sprRing.DOColor(Color.red, _fadeInDuration).ChangeStartValue(Color.white));
            _sequence.Join(_sprCursor.DOColor(Color.red, _fadeInDuration).ChangeStartValue(Color.white));
            _sequence.Join(DOVirtual.Float(0f, 1f, _fadeInDuration, UpdateRingMaterial).SetUpdate(false));

            _sequence.AppendInterval(_fadeStopDuration);

            _sequence.Append(_sprRing.DOColor(Color.white, _fadeInDuration).ChangeStartValue(Color.red));
            _sequence.Join(_sprCursor.DOColor(Color.white, _fadeInDuration).ChangeStartValue(Color.red));
            _sequence.Join(DOVirtual.Float(0f, 1f, _fadeOutDuration, UpdateRingMaterial).SetUpdate(false));

            _sequence.SetAutoKill(false);
            _sequence.Restart();
            _sequence.Pause();
        }

        void UpdateRingMaterial(float x)
        {
            _matRing.SetColor(ProID_EmisiveColor, _sprCursor.color);
        }

        void CharacterMovement_OnSpeedChanged(float speed)
        {
            if (speed <= 0 || PlayerScript.Instance.IsHiding)
                _radius = 0f;
            else
                _radius = Mathf.Lerp(_radiusRange.x, _radiusRange.y, speed);

            //_spriteRenderer.color = _colorRange.Evaluate(speed);
            _tfRing.SetScale(_radius + _radiusExtend);
        }

        void GameEvent_PlayerDetected()
        {
            if (_sequence.IsPlaying())
                return;

            _sequence.Restart();
            _sequence.Play();
        }
    }
}