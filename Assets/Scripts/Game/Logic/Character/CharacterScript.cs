using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    public class CharacterScript : CacheMonoBehaviour
    {
        Vector3 _startPosition;
        Vector3 _startEuler;

        CharacterMovement _characterMovement;
        CharacterRenderer _characterRenderer;

        Tween _tween;

        public CharacterMovement CharacterMovement { get { return _characterMovement; } }

        public Vector3 StartPosition { get { return _startPosition; } }
        public Vector3 StartEuler { get { return _startEuler; } }

        #region MonoBehaviour

        void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _characterRenderer = GetComponent<CharacterRenderer>();
        }

        void Start()
        {
            _startPosition = CacheTransform.position;
            _startEuler = CacheTransform.eulerAngles;
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        #endregion

        #region Public

        public bool WalkTo(Vector3 position)
        {
            bool moveSuccess = _characterMovement.MoveTo(position);

            if (moveSuccess)
                _characterRenderer.PlayWalk();

            return moveSuccess;
        }

        public void RunTo(Vector3 position)
        {
            _characterMovement.MoveTo(position);
            _characterRenderer.PlayRun();
        }

        public void StopMove()
        {
            _characterMovement.Stop();
            _characterRenderer.PlayIdle();
        }

        public void PlayCapture()
        {
            _characterMovement.Stop();
            _characterRenderer.PlayAttack();
        }

        public void PlaySleep()
        {
            _characterMovement.Stop();
            _characterRenderer.PlaySleep();
        }

        public void PlayAwake()
        {
            _characterRenderer.PlayAwake();
        }

        public void SetFreeze(bool isFreeze)
        {
            _characterRenderer.SetSpeed(isFreeze ? 0f : 1f);
            _characterMovement.SetEnabled(!isFreeze);
        }

        public void SetMoveSpeed(float speed)
        {
            _characterMovement.SetSpeed(speed);
        }

        public void RotateToOrigin()
        {
            _tween = CacheTransform.DORotate(_startEuler, 0.5f);
        }

        #endregion
    }
}