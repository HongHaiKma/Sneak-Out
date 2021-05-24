using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    [SelectionBase]
    public class ItemIce : CacheMonoBehaviour, IPlayerCollidable
    {
        static readonly float RespawnDelay = 5f;

        Tween _tween;
        Collider _collider;

        void Awake()
        {
            _collider = GetComponentInChildren<Collider>();
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        #region IPlayerCollidable

        void IPlayerCollidable.OnTriggerEnter(PlayerScript player)
        {
            Messenger.Broadcast(GameEvent.Item_Ice);

            CacheGameObject.SetActive(false);

            _collider.enabled = false;

            _tween = DOVirtual.DelayedCall(RespawnDelay, Respawn, false);
        }

        void IPlayerCollidable.OnTriggerExit(PlayerScript player)
        {
        }

        #endregion

        void Respawn()
        {
            CacheTransform.SetScale(0f);

            _tween = CacheTransform.DOScale(1f, 0.7f)
                .OnComplete(() => { _collider.enabled = true; });

            CacheGameObject.SetActive(true);
        }
    }
}