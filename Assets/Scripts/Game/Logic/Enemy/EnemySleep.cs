using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    public class EnemySleep : CacheMonoBehaviour, IStateMachine
    {
        CharacterEmote _characterEmote;
        CharacterScript _characterScript;
        GameObject _objEffect;
        Tween _tween;

        public event Callback OnWakeUp;

        void Awake()
        {
            _characterScript = GetComponent<CharacterScript>();
            _characterEmote = GetComponentInChildren<CharacterEmote>();
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        #region IStateMachine

        void IStateMachine.Init()
        {
            this.enabled = false;
        }

        void IStateMachine.OnStart()
        {
            this.enabled = true;

            _characterScript.PlaySleep();

            if (_objEffect == null)
                _objEffect = PrefabFactory.FxSleep.CreateRelativeLocal(CacheTransform);

            _objEffect.SetActive(true);
        }

        void IStateMachine.OnUpdate()
        {
        }

        void IStateMachine.OnStop()
        {
            if (_objEffect != null)
                _objEffect.SetActive(false);

            this.enabled = false;
        }

        #endregion

        public void WakeUp()
        {
            if (_tween.IsActive())
                return;

            _tween = DOVirtual.DelayedCall(1f, () => { OnWakeUp?.Invoke(); });
            _characterScript.PlayAwake();
            _characterEmote.PlaySuspect();
        }
    }
}