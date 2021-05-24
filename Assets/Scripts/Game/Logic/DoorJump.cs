using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    [SelectionBase]
    public class DoorJump : CacheMonoBehaviour, IPlayerCollidable
    {
        static readonly Color DisableColor = new Color(164f / 255f, 164f / 255f, 164f / 255f, 1f);

        [Header("Reference")]
        [SerializeField] Transform _tfLeftDoor;
        [SerializeField] Transform _tfRightDoor;
        [SerializeField] DoorJump _djLinked;

        [Header("Config")]
        [SerializeField] float _doorAngle = 110f;
        [SerializeField] Ease _openEase;
        [SerializeField] float _openDuration;
        [SerializeField] Ease _closeEase;
        [SerializeField] float _closeDuration;

        [SerializeField] float _inCloseDelay = 0.7f;
        [SerializeField] float _outCloseDelay = 0.1f;

        Sequence _sequence;
        Material _material;
        bool _waitToActive = false;

        #region MonoBehaviour

        void OnDestroy()
        {
            _sequence?.Kill();
        }

        #endregion

        #region IPlayerCollidable

        void IPlayerCollidable.OnTriggerEnter(PlayerScript player)
        {
            if (!this.enabled)
                return;

            PlaySequence(_inCloseDelay);

            player.JumpDoor(this, _djLinked);
        }

        void IPlayerCollidable.OnTriggerExit(PlayerScript player)
        {
            if (_waitToActive)
            {
                _material.color = Color.white;
                this.enabled = true;

                _waitToActive = false;
            }
        }

        #endregion

        Sequence PlaySequence(float closeDelay)
        {
            Vector3 leftAngle = new Vector3(0f, 0f, -_doorAngle);
            Vector3 rightAngle = new Vector3(0f, 0f, _doorAngle);

            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            _sequence.Append(_tfLeftDoor.DOLocalRotate(leftAngle, _openDuration).SetEase(_openEase).ChangeStartValue(Vector3.zero));
            _sequence.Join(_tfRightDoor.DOLocalRotate(rightAngle, _openDuration).SetEase(_openEase).ChangeStartValue(Vector3.zero));

            if (closeDelay > 0)
                _sequence.AppendInterval(closeDelay);

            _tfLeftDoor.localEulerAngles = leftAngle;
            _tfRightDoor.localEulerAngles = rightAngle;

            _sequence.Append(_tfLeftDoor.DOLocalRotate(Vector3.zero, _closeDuration).SetEase(_closeEase));
            _sequence.Join(_tfRightDoor.DOLocalRotate(Vector3.zero, _closeDuration).SetEase(_closeEase));

            _sequence.Play();

            return _sequence;
        }

        #region Public

        public void Construct(Material material)
        {
            _material = new Material(material);

            MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

            for (int i = 0; i < meshRenderers.Length; i++)
            {
                meshRenderers[i].sharedMaterial = _material;
            }
        }

        public void SetEnabled(bool enabled)
        {
            this.enabled = enabled;
        }

        public void PlayOpen()
        {
            PlaySequence(_outCloseDelay)
                .OnComplete(() =>
                {
                    _waitToActive = true;
                    _material.color = DisableColor;
                });

        }

        #endregion
    }
}