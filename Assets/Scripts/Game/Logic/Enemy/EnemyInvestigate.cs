using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    public class EnemyInvestigate : MonoBehaviour, IStateMachine
    {
        [Header("Config")]
        [SerializeField] float _stopDuration;

        CharacterScript _characterScript;
        CharacterEmote _characterEmote;
        EnemyDetector _enemyDetector;
        Tween _tween;

        public event Callback OnComplete;

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

        void OnDestroy()
        {
            _tween?.Kill();
        }

        #region IStateMachine

        void IStateMachine.Init()
        {
            this.enabled = false;
        }

        void IStateMachine.OnStart()
        {
            this.enabled = true;

            _characterScript.SetMoveSpeed(GameConfig.EnemyWalkSpeed);
            _characterScript.WalkTo(_enemyDetector.LastKnowPosition);
        }

        void IStateMachine.OnUpdate()
        {
        }

        void IStateMachine.OnStop()
        {
            this.enabled = false;

            _tween?.Kill();
        }

        #endregion

        void CharacterMovement_OnDestinationReached()
        {
            if (!this.enabled)
                return;

            _characterScript.StopMove();

            _characterEmote.PlaySuspect();

            _tween = DOVirtual.DelayedCall(_stopDuration, () => { OnComplete?.Invoke(); }, false);
        }
    }
}