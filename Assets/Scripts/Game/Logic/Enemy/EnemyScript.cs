using UnityEngine;
using PFramework;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Game
{
    [SelectionBase]
    public class EnemyScript : MonoBehaviour
    {
        static int Order = 0;

        enum State
        {
            Idle,
            Sleep,
            Patrol,
            Investigate,
            Reaction,
            Chase,
            Capture,
            Freeze,
        }

        [System.Serializable]
        public enum Behaviour
        {
            Idle,
            Patrol,
            Sleep,
        }

        [Header("Config")]
        [SerializeField] Behaviour _behaviour;
        [ShowIf("@_behaviour==Behaviour.Patrol")]
        [ListDrawerSettings(AddCopiesLastElement = true)]
        [SerializeField] List<EnemyPatrol.Node> _patrolNodes;
        [ShowIf("@_behaviour==Behaviour.Patrol")]
        [SerializeField] bool _patrolLoopRestart;

        StateMachine<State> _stateMachine;
        EnemyDetector _enemyDetector;
        EnemyReaction _enemyReaction;
        EnemyInvestigate _enemyInvestigate;
        EnemySleep _enemySleep;

        public Behaviour EnemyBehaviour { get { return _behaviour; } }
        public List<EnemyPatrol.Node> PatrolNodes { get { return _patrolNodes; } }
        public bool PatrolLoopRestart { get { return _patrolLoopRestart; } }

        #region MonoBehaviour

        void Awake()
        {
            // Get components
            _enemyDetector = GetComponent<EnemyDetector>();
            _enemyReaction = GetComponent<EnemyReaction>();
            _enemyInvestigate = GetComponent<EnemyInvestigate>();
            _enemySleep = GetComponent<EnemySleep>();
            EnemyIdle enemyIdle = GetComponent<EnemyIdle>();
            EnemyChase enemyChase = GetComponent<EnemyChase>();
            EnemyPatrol enemyPatrol = GetComponent<EnemyPatrol>();
            EnemyFreeze enemyFreeze = GetComponent<EnemyFreeze>();

            // Subscribe event from enemy detector
            _enemyDetector.OnPlayerNoiseDetected += EnemyDetector_OnPlayerNoiseDetected;
            _enemyDetector.OnPlayerSeen += EnemyDetector_OnPlayerSeen;
            _enemyDetector.OnPlayerInCaptureRange += EnemyDetector_OnPlayerInCaptureRange;

            // Subscribe event from enemy reaction
            _enemyReaction.OnDetectedComplete += EnemyReaction_OnDetectedComplete;
            _enemyReaction.OnSuspectComplete += EnemyReaction_OnSuspectComplete;

            // Subscribe event from enemy investigate
            _enemyInvestigate.OnComplete += EnemyInvestigate_OnComplete;

            // Subscribe event from enemy sleep
            _enemySleep.OnWakeUp += EnemySleep_OnWakeUp;

            enemyChase.OnLostTrack += EnemyChase_OnLostTrack;
            enemyFreeze.OnEnd += EnemyFreeze_OnEnd;

            _stateMachine = new StateMachine<State>();
            _stateMachine.AddState(State.Idle, enemyIdle);
            _stateMachine.AddState(State.Chase, enemyChase);
            _stateMachine.AddState(State.Capture, GetComponent<EnemyCapture>());
            _stateMachine.AddState(State.Patrol, enemyPatrol);
            _stateMachine.AddState(State.Sleep, GetComponent<EnemySleep>());
            _stateMachine.AddState(State.Reaction, _enemyReaction);
            _stateMachine.AddState(State.Investigate, _enemyInvestigate);
            _stateMachine.AddState(State.Freeze, enemyFreeze);

            _stateMachine.OnStateChanged += StateMachine_OnStateChanged;
        }

        void Start()
        {
            GetComponent<CharacterMovement>().SetPriority(Order);
            Order++;

            Messenger.AddListener<bool>(GameEvent.Game_End, GameEvent_GameEnd);
            Messenger.AddListener(GameEvent.Item_Ice, GameEvent_ItemIce);

            StartCoroutine(IEStart());
        }

        void OnDestroy()
        {
            Messenger.RemoveListener<bool>(GameEvent.Game_End, GameEvent_GameEnd);
            Messenger.RemoveListener(GameEvent.Item_Ice, GameEvent_ItemIce);
        }

        void Update()
        {
            _stateMachine.Update();
        }

        #endregion

        void EnemyInvestigate_OnComplete()
        {
            BackToOriginalState();
        }

        void EnemyChase_OnLostTrack()
        {
            _stateMachine.CurrentState = State.Investigate;
        }

        void EnemyDetector_OnPlayerSeen()
        {
            switch (_stateMachine.CurrentState)
            {
                case State.Idle:
                case State.Patrol:
                case State.Reaction:
                case State.Investigate:
                    _enemyReaction.Dectected();

                    if (_stateMachine.CurrentState != State.Reaction)
                        _stateMachine.CurrentState = State.Reaction;
                    break;
            }
        }

        void EnemyDetector_OnPlayerNoiseDetected()
        {
            switch (_stateMachine.CurrentState)
            {
                case State.Investigate:
                    EnemyDetector_OnPlayerSeen();
                    break;
                case State.Idle:
                case State.Patrol:
                    _enemyReaction.Suspect();

                    if (_stateMachine.CurrentState != State.Reaction)
                        _stateMachine.CurrentState = State.Reaction;
                    break;
                case State.Sleep:
                    _enemySleep.WakeUp();
                    break;
            }
        }

        void EnemyDetector_OnPlayerInCaptureRange()
        {
            switch (_stateMachine.CurrentState)
            {
                case State.Sleep:
                case State.Capture:
                    break;
                default:
                    _stateMachine.CurrentState = State.Capture;
                    break;
            }
        }

        void EnemyReaction_OnSuspectComplete()
        {
            _stateMachine.CurrentState = State.Investigate;
        }

        void EnemyReaction_OnDetectedComplete()
        {
            _stateMachine.CurrentState = State.Chase;
        }

        void EnemySleep_OnWakeUp()
        {
            _stateMachine.CurrentState = State.Investigate;
        }

        void EnemyFreeze_OnEnd()
        {
            _enemyReaction.Suspect();

            BackToOriginalState();
        }

        void StateMachine_OnStateChanged()
        {
            switch (_stateMachine.CurrentState)
            {
                case State.Sleep:
                    _enemyDetector.SetSeeEnabled(false);
                    _enemyDetector.SetEnabled(true);
                    break;
                case State.Idle:
                case State.Patrol:
                case State.Chase:
                case State.Reaction:
                case State.Investigate:
                    _enemyDetector.SetEnabled(true);
                    _enemyDetector.SetSeeEnabled(true);
                    break;
                default:
                    _enemyDetector.SetEnabled(false);
                    _enemyDetector.SetSeeEnabled(false);
                    break;
            }
        }

        void GameEvent_GameEnd(bool isWin)
        {
            if (!isWin)
            {
                switch (_stateMachine.CurrentState)
                {
                    case State.Sleep:
                    case State.Capture:
                        break;
                    default:
                        _stateMachine.CurrentState = State.Idle;
                        break;
                }

                this.enabled = false;
            }
            else
            {
                gameObject.SetActive(false);

                PrefabFactory.FxSmoke.CreateRelative(transform.position);
            }

        }

        void GameEvent_ItemIce()
        {
            _stateMachine.CurrentState = State.Freeze;
        }

        IEnumerator IEStart()
        {
            yield return new WaitForEndOfFrame();

            BackToOriginalState();
        }

        void BackToOriginalState()
        {
            switch (_behaviour)
            {
                case Behaviour.Idle:
                    _stateMachine.CurrentState = State.Idle;
                    break;

                case Behaviour.Patrol:
                    _stateMachine.CurrentState = State.Patrol;
                    break;
                case Behaviour.Sleep:
                    _stateMachine.CurrentState = State.Sleep;
                    break;
            }
        }
    }
}