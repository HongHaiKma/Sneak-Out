using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    public class PlayerAttack : CacheMonoBehaviour, IStateMachine
    {
        CharacterMovement _characterMovement;
        CharacterRenderer _characterRenderer;
        // RagdollScript _ragdollScript;

        PlayerScript _playerScript;

        Vector3 _pushPosition;

        void Awake()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _characterRenderer = GetComponent<CharacterRenderer>();
            // _ragdollScript = GetComponent<RagdollScript>();
            _playerScript = GetComponent<PlayerScript>();
        }

        public void Construct(Vector3 pushPosition)
        {
            _pushPosition = pushPosition;
        }

        public void KillEnemy()
        {
            _characterMovement.SetEnabled(false);
            _playerScript.m_EnemyScript.EnemyDown_OnDown();
            CacheTransform.DOMove(_playerScript.Position, 0.25f).OnComplete
            (
                () =>
                {
                    _characterMovement.SetEnabled(true);
                }
            );
        }

        #region IStateMachine

        void IStateMachine.Init()
        {
        }

        void IStateMachine.OnStart()
        {
            _characterRenderer.SetTrigger(HashDictionary.Attack);
        }

        void IStateMachine.OnStop()
        {
        }

        void IStateMachine.OnUpdate()
        {
            // KillEnemy();
        }

        #endregion
    }
}