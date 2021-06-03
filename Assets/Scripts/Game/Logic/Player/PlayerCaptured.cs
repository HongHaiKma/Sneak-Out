using PFramework;
using UnityEngine;

namespace Game
{
    public class PlayerCaptured : CacheMonoBehaviour, IStateMachine
    {
        CharacterMovement _characterMovement;
        CharacterRenderer _characterRenderer;
        // RagdollScript _ragdollScript;

        Vector3 _pushPosition;

        void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _characterRenderer = GetComponent<CharacterRenderer>();
            // _ragdollScript = GetComponent<RagdollScript>();
        }

        public void Construct(Vector3 pushPosition)
        {
            _pushPosition = pushPosition;
            // _characterRenderer.SetTrigger(HashDictionary.Die);
        }

        #region IStateMachine

        void IStateMachine.Init()
        {
            // _characterRenderer.SetTrigger(HashDictionary.Die);
        }

        void IStateMachine.OnStart()
        {
            _characterRenderer.SetTrigger(HashDictionary.Die);
            _characterMovement.SetEnabled(false);
            // _characterRenderer.SetEnabled(false);
            // _ragdollScript.SetEnabled(true);

            // _ragdollScript.AddForceCenter((CacheTransform.position - _pushPosition).normalized * 10000f, _pushPosition);
            // _ragdollScript.GetComponent<Rigidbody>().AddForce((CacheTransform.position - _pushPosition).normalized * 10000f, ForceMode.Impulse);
            GetComponent<Rigidbody>().AddForce((CacheTransform.position - _pushPosition).normalized * 10000f, ForceMode.Impulse);

            Helper.DebugLog("Player caupterd!!!!!!!!!!!!!!!");
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