using PFramework;
using UnityEngine;

namespace Game
{
    public class EnemyFreeze : CacheMonoBehaviour, IStateMachine
    {
        CharacterScript _characterScript;

        public event Callback OnEnd;

        float _timeRemain = 0f;

        void Awake()
        {
            _characterScript = GetComponent<CharacterScript>();
        }

        #region IStateMachine

        void IStateMachine.Init()
        {
        }

        void IStateMachine.OnStart()
        {
            _characterScript.SetFreeze(true);

            _timeRemain = GameConfig.EnemyFreezeDuration;

            PrefabFactory.FxIce.CreateRelative(CacheTransform);
        }

        void IStateMachine.OnUpdate()
        {
            _timeRemain -= Time.deltaTime;

            if (_timeRemain <= 0f)
            {
                _characterScript.SetFreeze(false);

                OnEnd?.Invoke();
            }
        }

        void IStateMachine.OnStop()
        {
        }


        #endregion
    }
}