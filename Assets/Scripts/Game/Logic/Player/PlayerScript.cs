using PFramework;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

namespace Game
{
    public class PlayerScript : Singleton<PlayerScript>
    {
        enum State
        {
            Normal,
            JumpDoor,
            Captured,
            Victory,
            Attack,
        }

        public EnemyScript m_EnemyScript;

        CharacterMovement _characterMovement;
        // RagdollScript _ragdollScript;
        PlayerJumpDoor _playerJumpDoor;
        PlayerCaptured _playerCaptured;

        PlayerAttack _playerAttack;

        StateMachine<State> _stateMachine;

        public bool IsHiding { get; set; }

        #region MonoBehaviour

        protected override void Awake()
        {
            base.Awake();

            // _ragdollScript = GetComponent<RagdollScript>();
            _characterMovement = GetComponent<CharacterMovement>();
            _playerJumpDoor = GetComponent<PlayerJumpDoor>();
            _playerCaptured = GetComponent<PlayerCaptured>();

            _playerAttack = GetComponent<PlayerAttack>();


            _playerJumpDoor.OnJumpOutComplete += PlayerJumpDoor_OnJumpOutComplete;

            // _ragdollScript.SetEnabled(false);
        }

        void Start()
        {
            _stateMachine = new StateMachine<State>();
            _stateMachine.AddState(State.Normal, GetComponent<PlayerNormal>());
            _stateMachine.AddState(State.JumpDoor, _playerJumpDoor);
            _stateMachine.AddState(State.Captured, _playerCaptured);
            _stateMachine.AddState(State.Victory, GetComponent<PlayerVictory>());

            _stateMachine.AddState(State.Attack, _playerAttack);

            _stateMachine.CurrentState = State.Normal;
        }

        void Update()
        {
            _stateMachine.Update();

            // Collider[] cols = Physics.OverlapSphere(Position, 6f);
            // List<EnemyScript> enemies = new List<EnemyScript>();
            // for (int i = 0; i < cols.Length; i++)
            // {
            //     EnemyScript enemyScript = cols[i].GetComponent<EnemyScript>();
            //     if (enemyScript != null)
            //     {
            //         enemies.Add(enemyScript);
            //     }
            // }

            // if (enemies.Count == 1)
            // {
            //     m_EnemyScript = enemies[0];
            // }
            // else if (enemies.Count > 1)
            // {
            //     float distance = 10000000f;
            //     for (int i = 0; i < enemies.Count; i++)
            //     {
            //         if (Helper.CalDistance(Position, enemies[i].transform.position) < distance)
            //         {
            //             distance = Helper.CalDistance(Position, enemies[i].transform.position);
            //             m_EnemyScript = enemies[i];
            //         }
            //     }
            // }

            // if (m_EnemyScript != null)
            // {
            //     if (Helper.InRange(Position, m_EnemyScript.transform.position, 2.5f))
            //     {
            //         m_EnemyScript.EnemyDown_OnDown();
            //     }
            // }
        }

        public bool IsGoodToKillEnemy()
        {
            Collider[] cols = Physics.OverlapSphere(Position, 6f);
            List<EnemyScript> enemies = new List<EnemyScript>();
            for (int i = 0; i < cols.Length; i++)
            {
                EnemyScript enemyScript = cols[i].GetComponent<EnemyScript>();
                if (enemyScript != null)
                {
                    enemies.Add(enemyScript);
                }
            }

            if (enemies.Count == 1)
            {
                m_EnemyScript = enemies[0];
            }
            else if (enemies.Count > 1)
            {
                float distance = 10000000f;
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (Helper.CalDistance(Position, enemies[i].transform.position) < distance)
                    {
                        distance = Helper.CalDistance(Position, enemies[i].transform.position);
                        m_EnemyScript = enemies[i];
                    }
                }
            }

            if (m_EnemyScript != null)
            {
                if (m_EnemyScript.IsCannotDown())
                {
                    m_EnemyScript = null;
                    return false;
                }
                else
                {
                    if (Helper.InRange(Position, m_EnemyScript.transform.position, 4.5f))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        public void KillEnemy()
        {
            _characterMovement.SetEnabled(false);
            m_EnemyScript.EnemyDown_OnDown();
            CacheTransform.DOMove(m_EnemyScript.Position, 0.3f).OnComplete
            (
                () =>
                {
                    _characterMovement.SetEnabled(true);
                    _stateMachine.CurrentState = State.Normal;
                    ProfileManager.AddGold(10);
                    EventManager.CallEvent(GameEvents.UPDATE_GOLD);
                }
            );
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(Position, 6f);
        }

        void OnTriggerEnter(Collider other)
        {
            IPlayerCollidable collidable = other.GetComponent<IPlayerCollidable>();

            if (collidable == null)
                collidable = other.GetComponentInParent<IPlayerCollidable>();

            if (collidable != null)
                collidable.OnTriggerEnter(this);
        }

        private void OnTriggerExit(Collider other)
        {
            IPlayerCollidable collidable = other.GetComponent<IPlayerCollidable>();

            if (collidable == null)
                collidable = other.GetComponentInParent<IPlayerCollidable>();

            if (collidable != null)
                collidable.OnTriggerExit(this);
        }

        #endregion

        public void AttackEnemy()
        {
            _stateMachine.CurrentState = State.Attack;
        }

        public void GoalReached()
        {
            _stateMachine.CurrentState = State.Victory;
        }

        public void Capture(Vector3 position)
        {
            _playerCaptured.Construct(position);

            _stateMachine.CurrentState = State.Captured;
        }

        public void JumpDoor(DoorJump start, DoorJump end)
        {
            if (_stateMachine.CurrentState == State.JumpDoor)
                return;

            _playerJumpDoor.Construct(start, end);

            _stateMachine.CurrentState = State.JumpDoor;
        }

        void PlayerJumpDoor_OnJumpOutComplete()
        {
            _stateMachine.CurrentState = State.Normal;
        }
    }
}