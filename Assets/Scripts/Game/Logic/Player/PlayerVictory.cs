using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    public class PlayerVictory : CacheMonoBehaviour, IStateMachine
    {
        CharacterRenderer _characterRenderer;
        CharacterMovement _characterMovement;

        Tween _tween;

        void Awake()
        {
            _characterRenderer = GetComponent<CharacterRenderer>();
            _characterMovement = GetComponent<CharacterMovement>();
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        #region IStateMachine

        void IStateMachine.Init()
        {
        }

        void IStateMachine.OnStart()
        {
            _characterMovement.SetEnabled(false);
            _characterRenderer.PlayVictory();

            _tween = CacheTransform.DORotate(new Vector3(0f, 135f, 0f), 0.5f);
        }

        void IStateMachine.OnStop()
        {
        }

        void IStateMachine.OnUpdate()
        {
        }

        #endregion
    }
}