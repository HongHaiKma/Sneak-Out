using PFramework;
using UnityEngine;

namespace Game
{
    public class PlayerScript : Singleton<PlayerScript>
    {
        enum State
        {
            Normal,
            JumpDoor,
            Captured,
            Victory,
        }

        CharacterMovement _characterMovement;
        RagdollScript _ragdollScript;
        PlayerJumpDoor _playerJumpDoor;
        PlayerCaptured _playerCaptured;

        StateMachine<State> _stateMachine;

        public bool IsHiding { get; set; }

        #region MonoBehaviour

        protected override void Awake()
        {
            base.Awake();

            _ragdollScript = GetComponent<RagdollScript>();
            _characterMovement = GetComponent<CharacterMovement>();
            _playerJumpDoor = GetComponent<PlayerJumpDoor>();
            _playerCaptured = GetComponent<PlayerCaptured>();

            _playerJumpDoor.OnJumpOutComplete += PlayerJumpDoor_OnJumpOutComplete;

            _ragdollScript.SetEnabled(false);
        }

        void Start()
        {
            _stateMachine = new StateMachine<State>();
            _stateMachine.AddState(State.Normal, GetComponent<PlayerNormal>());
            _stateMachine.AddState(State.JumpDoor, _playerJumpDoor);
            _stateMachine.AddState(State.Captured, _playerCaptured);
            _stateMachine.AddState(State.Victory, GetComponent<PlayerVictory>());

            _stateMachine.CurrentState = State.Normal;
        }

        void Update()
        {
            _stateMachine.Update();
        }

        void OnTriggerEnter(Collider other)
        {
            IPlayerCollidable collidable = other.GetComponent<IPlayerCollidable>();

            if (collidable == null)
                collidable = other.GetComponentInParent<IPlayerCollidable>();

            if (collidable != null)
                collidable.OnTriggerEnter(this);
        }

        private void OnTriggerExit(Collider other)
        {
            IPlayerCollidable collidable = other.GetComponent<IPlayerCollidable>();

            if (collidable == null)
                collidable = other.GetComponentInParent<IPlayerCollidable>();

            if (collidable != null)
                collidable.OnTriggerExit(this);
        }

        #endregion

        public void GoalReached()
        {
            _stateMachine.CurrentState = State.Victory;
        }

        public void Capture(Vector3 position)
        {
            _playerCaptured.Construct(position);

            _stateMachine.CurrentState = State.Captured;
        }

        public void JumpDoor(DoorJump start, DoorJump end)
        {
            if (_stateMachine.CurrentState == State.JumpDoor)
                return;

            _playerJumpDoor.Construct(start, end);

            _stateMachine.CurrentState = State.JumpDoor;
        }

        void PlayerJumpDoor_OnJumpOutComplete()
        {
            _stateMachine.CurrentState = State.Normal;
        }
    }
}