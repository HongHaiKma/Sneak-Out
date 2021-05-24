using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class RagdollScript : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] Rigidbody[] _rigidbodies;
        [SerializeField] Rigidbody _rigidbodyCenter;

        [Button]
        protected void GetAllRigid()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
        }

        public void SetEnabled(bool enabled)
        {
            for (int i = 0; i < _rigidbodies.Length; i++)
            {
                _rigidbodies[i].isKinematic = !enabled;
            }
        }

        public void AddForceCenter(Vector3 force, Vector3 position)
        {
            _rigidbodyCenter.AddForceAtPosition(force, position, ForceMode.Force);
        }
    }
}