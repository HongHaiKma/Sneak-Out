using PFramework;
using UnityEngine;

namespace Game
{
    public class PlayerNormal : MonoBehaviour, IStateMachine
    {
        CharacterMovement _characterMovement;

        #region MonoBehaviour

        void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();

            PlayerControl playerControl = GetComponent<PlayerControl>();

            playerControl.OnMove += PlayerControl_OnMove;
            playerControl.OnStop += PlayerControl_OnStop;
        }

        #endregion

        void PlayerControl_OnStop()
        {
            if (!this.enabled)
                return;

            _characterMovement.SpeedNormalized = 0f;
        }

        void PlayerControl_OnMove(Vector3 dir, float speedNormalized)
        {
            if (!this.enabled)
                return;

            _characterMovement.Direction = dir;
            _characterMovement.SpeedNormalized = speedNormalized;
        }

        #region IStateMachine

        void IStateMachine.Init()
        {
            this.enabled = false;
        }

        void IStateMachine.OnStart()
        {
            this.enabled = true;
        }

        void IStateMachine.OnUpdate()
        {
        }

        void IStateMachine.OnStop()
        {
            this.enabled = false;
        }

        #endregion
    }
}