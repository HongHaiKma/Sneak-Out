using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    public class CameraCinematic : CacheMonoBehaviour
    {
        CameraView _cameraView;
        Tween _tween;

        #region MonoBehaviour

        void Awake()
        {
            _cameraView = GetComponentInChildren<CameraView>();
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        #endregion

        public Sequence Play(Vector3 offsetPos, Vector3 offsetRot, float duration, float delay, Ease ease)
        {
            Vector3 startPos = _cameraView.OffsetPos;
            Vector3 startRot = _cameraView.OffsetRot;

            _tween?.Kill();
            Sequence sequence = DOTween.Sequence();

            sequence.AppendInterval(delay);
            sequence.Append(DOTween.To(() => startPos, (x) => { startPos = x; UpdatePos(startPos); }, offsetPos, duration).SetEase(ease));
            sequence.Join(DOTween.To(() => startRot, (x) => { startRot = x; UpdateRot(startRot); }, offsetRot, duration).SetEase(ease));
            sequence.OnComplete(Sequence_Complete);

            _tween = sequence;

            return sequence;
        }

        public Tween Play(Vector3[] localPositions, float duration, Ease ease)
        {
            Transform target = new GameObject().transform;
            target.SetParent(CacheTransform);
            target.localPosition = CacheTransform.localPosition;

            _tween?.Kill();
            _tween = target.DOLocalPath(localPositions, duration, PathType.CatmullRom, gizmoColor: Color.red)
                .SetEase(ease)
                .OnUpdate(() => { UpdatePos(target.localPosition); })
                .OnKill(() => { if (target != null) Destroy(target.gameObject); });

            return _tween;
        }

        public void RotateAround()
        {
            _tween?.Kill();

            _tween = _cameraView.TfRoot.DORotate(new Vector3(0f, 180f, 0f), 1.5f)
               .SetEase(Ease.Linear)
               .SetLoops(-1, LoopType.Incremental);
        }

        void Sequence_Complete()
        {
            //_cameraFollow.enabled = false;
            //CacheTransform.SetParent(_cameraFollow.TfTarget);

            //_sequence = _cameraFollow.TfTarget.DORotate(new Vector3(0f, 180f, 0f), 1.5f)
            //     .SetEase(Ease.Linear)
            //     .SetLoops(-1, LoopType.Incremental);
        }

        void UpdatePos(Vector3 pos)
        {
            _cameraView.OffsetPos = pos;
        }

        void UpdateRot(Vector3 rot)
        {
            _cameraView.OffsetRot = rot;
        }
    }
}