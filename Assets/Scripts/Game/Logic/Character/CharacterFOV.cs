using PFramework;
using UnityEngine;

namespace Game
{
    public class CharacterFOV : CacheMonoBehaviour
    {
        [Header("Config")]
        [SerializeField] float _radius;
        [SerializeField] float _angle;
        [SerializeField] LayerMask _lmObstacle;

        FOVRenderer _fovRenderer;

        void Awake()
        {
            _fovRenderer = GetComponentInChildren<FOVRenderer>();

            _fovRenderer.Construct(_radius, _angle, _lmObstacle);
        }

        #region Public

        public bool IsTagetInRange(Transform tfTarget)
        {
            float distance = Vector3.Distance(tfTarget.position, CacheTransform.position);

            if (distance > _radius)
                return false;

            if (Mathf.Abs(Vector3.SignedAngle(CacheTransform.forward, tfTarget.position - CacheTransform.position, Vector3.up)) <= _angle * 0.5f)
            {
                Vector3 dir = tfTarget.position - CacheTransform.position;

                if (!Physics.Raycast(CacheTransform.position, dir, distance, _lmObstacle))
                    return true;
            }

            return false;
        }

        public void SetEnabled(bool enabled)
        {
            this.enabled = enabled;

            _fovRenderer.gameObject.SetActive(enabled);
        }

        #endregion

#if UNITY_EDITOR

        void OnDrawGizmos()
        {
            GizmosHelper.DrawWireArc(CacheTransform.position, Vector3.up, Quaternion.AngleAxis(-_angle * 0.5f, Vector3.up) * CacheTransform.forward, _angle, _radius, Color.yellow);
        }

#endif
    }
}