using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    public class EnemyDown : MonoBehaviour, IStateMachine
    {
        [Header("Config")]
        [SerializeField] float _stopDuration;

        CharacterScript _characterScript;
        CharacterEmote _characterEmote;
        EnemyDetector _enemyDetector;
        Tween _tween;

        CharacterRenderer _charRender;

        public float m_EnemyDieTime;
        public float m_EnemyDieTimeMax;

        public event Callback OnComplete;

        void Awake()
        {
            // _characterScript = GetComponent<CharacterScript>();
            // _characterEmote = GetComponentInChildren<CharacterEmote>();
            // _enemyDetector = GetComponent<EnemyDetector>();
            _charRender = GetComponent<CharacterRenderer>();
        }

        void Start()
        {
            // _characterScript.CharacterMovement.OnDestinationReached += CharacterMovement_OnDestinationReached;
        }

        private void OnEnable()
        {
            m_EnemyDieTime = 0f;
            m_EnemyDieTimeMax = 2f;
        }

        void OnDestroy()
        {
            // _tween?.Kill();
        }

        #region IStateMachine

        void IStateMachine.Init()
        {
            // this.enabled = false;
        }

        void IStateMachine.OnStart()
        {
            // this.enabled = true;

            // _characterScript.SetMoveSpeed(GameConfig.EnemyWalkSpeed);
            // _characterScript.WalkTo(_enemyDetector.LastKnowPosition);

            _charRender.SetTrigger(HashDictionary.Die);
        }

        void IStateMachine.OnUpdate()
        {
            m_EnemyDieTime += Time.deltaTime;

            if (m_EnemyDieTime >= m_EnemyDieTimeMax)
            {
                OnComplete?.Invoke();
            }
        }

        void IStateMachine.OnStop()
        {
            // this.enabled = false;

            // _tween?.Kill();
        }

        #endregion

        void EnemyReaction_OnDownComplete()
        {
            // if (!this.enabled)
            //     return;

            // _characterScript.StopMove();

            // _characterEmote.PlaySuspect();

            // _tween = DOVirtual.DelayedCall(_stopDuration, () => { OnComplete?.Invoke(); }, false);
        }
    }
}