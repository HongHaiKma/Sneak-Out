using DG.Tweening;
using UnityEngine;

namespace Game
{
    [SelectionBase]
    public class DoorSwitchButton : MonoBehaviour, IPlayerCollidable
    {
        static readonly Color DisableColor = new Color(164f / 255f, 164f / 255f, 164f / 255f, 1f);

        [Header("Reference")]
        [SerializeField] Transform _tfButton;

        [Header("Config")]
        [SerializeField] float _y;
        [SerializeField] float _duration;
        [SerializeField] Ease _ease;

        DoorSwitch _doorSwitch;
        Tween _tween;
        bool _isSwitched = false;
        Material _material;

        void OnDestroy()
        {
            _tween?.Kill();
        }

        public void Construct(DoorSwitch doorSwitch, Material material)
        {
            _doorSwitch = doorSwitch;

            _material = new Material(material);

            _tfButton.GetComponent<MeshRenderer>().sharedMaterial = _material;
        }

        #region IPlayerCollidable

        void IPlayerCollidable.OnTriggerEnter(PlayerScript player)
        {
            if (_isSwitched)
                return;

            _isSwitched = true;

            _tween = _tfButton.DOLocalMoveY(_y, _duration)
                .SetEase(_ease)
                .OnComplete(() => { _material.color = DisableColor; });

            _doorSwitch.Open();
        }

        void IPlayerCollidable.OnTriggerExit(PlayerScript player)
        {
        }

        #endregion
    }
}