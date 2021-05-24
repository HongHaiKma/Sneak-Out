using PFramework;
using UnityEngine;

namespace Game
{
    public class EnemyReaction : MonoBehaviour, IStateMachine
    {
        enum State
        {
            Normal,
            Suspect,
            Detected,
        }

        [Header("Config")]
        [SerializeField] float _suspectDuration;
        [SerializeField] float _detectedDuration;

        CharacterEmote _characterEmote;
        CharacterMovement _characterMovement;
        CharacterRenderer _characterRenderer;
        State _state;
        float _time;

        public event Callback OnSuspectComplete;
        public event Callback OnDetectedComplete;

        #region MonoBehaviour

        void Awake()
        {
            _characterEmote = GetComponentInChildren<CharacterEmote>();
            _characterMovement = GetComponent<CharacterMovement>();
            _characterRenderer = GetComponent<CharacterRenderer>();
        }

        #endregion

        #region StateMachine

        void IStateMachine.Init()
        {
        }

        void IStateMachine.OnStart()
        {
            Messenger.Broadcast(GameEvent.Player_Detected);

            _characterMovement.Stop();
            _characterRenderer.PlayIdle();
        }

        void IStateMachine.OnUpdate()
        {
            _time -= Time.deltaTime;

            if (_time <= 0)
            {
                if (_state == State.Suspect)
                    OnSuspectComplete?.Invoke();
                else if (_state == State.Detected)
                    OnDetectedComplete?.Invoke();
            }
        }

        void IStateMachine.OnStop()
        {
            _state = State.Normal;
        }

        #endregion

        public void Suspect()
        {
            if (_state != State.Normal)
                return;

            _state = State.Suspect;
            _time = _suspectDuration;

            _characterEmote.PlaySuspect();
        }

        public void Dectected()
        {
            if (_state == State.Detected)
                return;

            _state = State.Detected;
            _time = _detectedDuration;

            _characterEmote.PlayDetected();
        }
    }
}