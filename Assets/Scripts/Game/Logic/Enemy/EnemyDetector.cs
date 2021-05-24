using PFramework;
using UnityEngine;

namespace Game
{
    public class EnemyDetector : CacheMonoBehaviour
    {
        [Header("Config")]
        [SerializeField] float _captureRadius;

        CharacterFOV _characterFOV;
        bool _canSee = true;

        public Vector3 LastKnowPosition { get; protected set; }

        public event Callback OnPlayerNoiseDetected;
        public event Callback OnPlayerSeen;
        public event Callback OnPlayerInCaptureRange;

        #region MonoBehaviour

        void Awake()
        {
            _characterFOV = GetComponent<CharacterFOV>();
        }

        void Start()
        {
            Messenger.AddListener<Vector3, float>(GameEvent.Player_Noise, GameEvent_PlayerNoise);

            this.enabled = false;
        }

        void Update()
        {
            if (PlayerScript.Instance.IsHiding)
                return;

            // Is player in capture range
            if ( Vector3.Distance(CacheTransform.position, PlayerScript.Instance.Position) <= _captureRadius)
            {
                OnPlayerInCaptureRange?.Invoke();
            }
            // Is player in FOV
            else if (_canSee && _characterFOV.IsTagetInRange(PlayerScript.Instance.CacheTransform))
            {
                LastKnowPosition = PlayerScript.Instance.Position;

                OnPlayerSeen?.Invoke();
            }
        }

        void OnDestroy()
        {
            Messenger.RemoveListener<Vector3, float>(GameEvent.Player_Noise, GameEvent_PlayerNoise);
        }

#if UNITY_EDITOR

        void OnDrawGizmos()
        {
            GizmosHelper.DrawCircleXZ(CacheTransform.position, _captureRadius, Color.red);
        }

#endif

        #endregion

        #region Public

        public void SetEnabled(bool enabled)
        {
            this.enabled = enabled;
        }

        public void SetSeeEnabled(bool enabled)
        {
            _canSee = enabled;
            _characterFOV.SetEnabled(enabled);
        }

        #endregion

        void GameEvent_PlayerNoise(Vector3 position, float radius)
        {
            if (!this.enabled)
                return;

            if (PlayerScript.Instance.IsHiding)
                return;

            if (Vector3.Distance(position, CacheTransform.position) <= radius)
            {
                LastKnowPosition = position;

                OnPlayerNoiseDetected?.Invoke();
            }
        }
    }
}