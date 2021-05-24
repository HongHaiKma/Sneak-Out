using PFramework;
using UnityEngine;

namespace Game
{
    public class Follower : CacheMonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] Transform _tfTarget;

        [Header("Config")]
        [SerializeField] bool _lockX;
        [SerializeField] bool _lockY;
        [SerializeField] bool _lockZ;

        void LateUpdate()
        {
            CacheTransform.position = new Vector3(
                _lockX ? CacheTransform.position.x : _tfTarget.position.x,
                _lockY ? CacheTransform.position.y : _tfTarget.position.y,
                _lockZ ? CacheTransform.position.z : _tfTarget.position.z);
        }
    }
}