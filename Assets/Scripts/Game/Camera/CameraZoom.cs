using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class CameraZoom : MonoBehaviour
    {
        CameraView _cameraView;
        Vector3 _originOffset;
        Tween _tween;

        #region MonoBehaviour

        void Awake()
        {
            _cameraView = GetComponentInChildren<CameraView>();
            _originOffset = _cameraView.OffsetPos;
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        #endregion

        public Tween Zoom(float scale, float duration)
        {
            _tween?.Kill();

            float _currentScale = _cameraView.OffsetPos.x / _originOffset.x;

            _tween = DOVirtual.Float(_currentScale, scale, duration, DoZoom);

            return _tween;
        }

        void DoZoom(float scale)
        {
            _cameraView.OffsetPos = _originOffset * scale;
        }
    }
}