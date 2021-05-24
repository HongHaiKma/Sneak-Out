using PFramework;
using UnityEngine;

namespace Game
{
    public class ShadowScript : CacheMonoBehaviour
    {
        static readonly float GroundY = 0.05f;

        [Header("Reference")]
        [SerializeField] Transform _tfTarget;
        [SerializeField] float _limitY = 0.6f;

        float _scale;

        void Awake()
        {
            _scale = CacheTransform.localScale.x;
        }

        void LateUpdate()
        {
            CacheTransform.position = new Vector3(_tfTarget.position.x, GroundY, _tfTarget.position.z);

            CacheTransform.SetScale(_tfTarget.position.y > _limitY ? _scale : 0f);
        }
    }
}