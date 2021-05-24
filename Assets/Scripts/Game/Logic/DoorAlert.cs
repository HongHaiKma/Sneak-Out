using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    public class DoorAlert : CacheMonoBehaviour, IPlayerCollidable
    {
        [Header("Reference")]
        [SerializeField] GameObject _objAlert;
        [SerializeField] Transform _tfRoot;

        [Header("Config")]
        [SerializeField] float _duration;

        Tween _tween;

        #region MonoBehaviour

        void Awake()
        {
            _objAlert.SetActive(false);
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        #endregion

        #region IPlayerCollidable

        void IPlayerCollidable.OnTriggerEnter(PlayerScript player)
        {
            Messenger.Broadcast(GameEvent.Player_Noise, _tfRoot.position, 99999f);

            _objAlert.SetActive(true);

            AudioManager.Play(AudioFactory.SfxAlarm);

            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(_duration, () => { _objAlert.SetActive(false); });

            PrefabFactory.FxRingNoise.Create(_tfRoot);
        }

        void IPlayerCollidable.OnTriggerExit(PlayerScript player)
        {
        }

        #endregion
    }
}