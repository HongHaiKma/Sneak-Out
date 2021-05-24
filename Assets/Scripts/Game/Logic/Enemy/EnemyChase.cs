using PFramework;
using UnityEngine;

namespace Game
{
    public class EnemyChase : CacheMonoBehaviour, IStateMachine
    {
        [Header("Config")]
        [SerializeField] float _chaseRadius;

        CharacterScript _characterScript;
        CharacterEmote _characterEmote;
        EnemyDetector _enemyDetector;

        public event Callback OnLostTrack;

        #region MonoBehaviour

        void Awake()
        {
            _characterScript = GetComponent<CharacterScript>();
            _characterEmote = GetComponentInChildren<CharacterEmote>();
            _enemyDetector = GetComponent<EnemyDetector>();
        }

        void Start()
        {
            _characterScript.CharacterMovement.OnDestinationReached += CharacterMovement_OnDestinationReached;
        }

#if UNITY_EDITOR

        void OnDrawGizmos()
        {
            GizmosHelper.DrawCircleXZ(CacheTransform.position, _chaseRadius, Color.white);
        }

#endif

        #endregion

        #region IStateMachine

        void IStateMachine.Init()
        {
            this.enabled = false;
        }

        void IStateMachine.OnStart()
        {
            this.enabled = true;

            _characterScript.RunTo(_enemyDetector.LastKnowPosition);
            _characterScript.SetMoveSpeed(GameConfig.EnemyRunSpeed);
        }

        void IStateMachine.OnUpdate()
        {
            if (PlayerScript.Instance.IsHiding)
                return;

            float distance = Vector3.Distance(CacheTransform.position, PlayerScript.Instance.Position);

            if (distance <= _chaseRadius)
            {
                _characterScript.RunTo(PlayerScript.Instance.Position);
            }
        }

        void IStateMachine.OnStop()
        {
            this.enabled = false;
        }

        #endregion

        void CharacterMovement_OnDestinationReached()
        {
            if (!this.enabled)
                return;

            _characterEmote.PlaySuspect();

            OnLostTrack?.Invoke();
        }
    }
}