using PFramework;
using UnityEngine;

namespace Game
{
    public class PlayerCaptured : CacheMonoBehaviour, IStateMachine
    {
        CharacterMovement _characterMovement;
        CharacterRenderer _characterRenderer;
        RagdollScript _ragdollScript;

        Vector3 _pushPosition;

        void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _characterRenderer = GetComponent<CharacterRenderer>();
            _ragdollScript = GetComponent<RagdollScript>();
        }

        public void Construct(Vector3 pushPosition)
        {
            _pushPosition = pushPosition;
        }

        #region IStateMachine

        void IStateMachine.Init()
        {
        }

        void IStateMachine.OnStart()
        {
            _characterMovement.SetEnabled(false);
            _characterRenderer.SetEnabled(false);
            _ragdollScript.SetEnabled(true);

            _ragdollScript.AddForceCenter((CacheTransform.position - _pushPosition).normalized * 10000f, _pushPosition);
        }

        void IStateMachine.OnStop()
        {
        }

        void IStateMachine.OnUpdate()
        {
        }

        #endregion
    }
}