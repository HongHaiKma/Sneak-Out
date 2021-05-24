using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    public class EnemyIdle : CacheMonoBehaviour, IStateMachine
    {
        CharacterScript _characterScript;

        #region MonoBehaviour

        void Awake()
        {
            _characterScript = GetComponent<CharacterScript>();
        }

        void Start()
        {
            _characterScript.CharacterMovement.OnDestinationReached += CharacterMovement_OnDestinationReached;
        }

        #endregion

        #region IStateMachine

        void IStateMachine.Init()
        {
            this.enabled = false;
        }

        void IStateMachine.OnStart()
        {
            if (CacheTransform.position != _characterScript.StartPosition)
                _characterScript.WalkTo(_characterScript.StartPosition);
            else
                _characterScript.StopMove();

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

        void CharacterMovement_OnDestinationReached()
        {
            if (!this.enabled)
                return;

            _characterScript.StopMove();
            _characterScript.RotateToOrigin();
        }
    }
}