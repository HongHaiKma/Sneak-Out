using PFramework;
using UnityEngine;

namespace Game
{
    public class CameraView : CacheMonoBehaviour
    {
        [Header("Config")]
        [SerializeField] Vector3 _offsetPos;
        [SerializeField] Vector3 _offsetRot;

        Transform _tfRoot;

        public Vector3 OffsetPos { get { return _offsetPos; } set { _offsetPos = value; UpdateView(); } }
        public Vector3 OffsetRot { get { return _offsetRot; } set { _offsetRot = value; } }
        public Transform TfRoot { get { return _tfRoot; } }

        #region MonoBehaviour

        void Awake()
        {
            _tfRoot = CacheTransform.parent;
        }

        void LateUpdate()
        {
            // Update position
            //CacheTransform.localPosition = Vector3.SmoothDamp(CacheTransform.localPosition, _offsetPos, ref _posVel, _smoothPositionSpeed);

            // Update rotation
            //Quaternion desireRotation = Quaternion.Euler(_offsetRot);
            //CacheTransform.localRotation = Quaternion.Lerp(CacheTransform.localRotation, desireRotation, _smoothRotationSpeed * Time.deltaTime);

            //CacheTransform.localPosition = _offsetPos;

            //CacheTransform.LookAt(_tfRoot);
            //CacheTransform.localEulerAngles += _offsetRot;
        }

        #endregion

        void UpdateView()
        {
            if (_tfRoot == null)
                return;

            CacheTransform.localPosition = _offsetPos;

            CacheTransform.LookAt(_tfRoot);
            CacheTransform.localEulerAngles += _offsetRot;
        }

#if UNITY_EDITOR

        void OnValidate()
        {
            _tfRoot = CacheTransform.parent;

            UpdateView();
        }

#endif
    }
}