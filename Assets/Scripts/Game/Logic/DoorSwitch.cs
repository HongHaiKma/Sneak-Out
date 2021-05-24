using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    [SelectionBase]
    public class DoorSwitch : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] GameObject _objCollider;
        [SerializeField] Transform _tfDoor;

        [Header("Config")]
        [SerializeField] float _y;
        [SerializeField] float _duration;
        [SerializeField] Ease _ease;

        Tween _tween;

        void OnDestroy()
        {
            _tween?.Kill();
        }

        public void Construct(Material material)
        {
            _tfDoor.GetComponent<MeshRenderer>().sharedMaterial = material;
        }

        public void Open()
        {
            _tween = _tfDoor.DOLocalMoveY(_y, _duration)
                .SetEase(_ease)
                .OnComplete(() => { _objCollider.SetActive(false); });

            AudioManager.Play(AudioFactory.SfxDoorOpen);
        }
    }
}