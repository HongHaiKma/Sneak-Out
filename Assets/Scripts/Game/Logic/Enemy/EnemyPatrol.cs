using DG.Tweening;
using PFramework;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class EnemyPatrol : CacheMonoBehaviour, IStateMachine
    {
        [System.Serializable]
        public class Node
        {
            [SerializeField] Vector3 _position;
            [SerializeField] float _stopDuration;

            public Vector3 Position { get { return _position; } set { _position = value; } }
            public float StopDuration { get { return _stopDuration; } }
        }

        CharacterScript _characterScript;
        EnemyScript _enemyScript;
        bool _isFoward = true;
        int _iNode = 0;
        Tween _tween;

        List<Node> _nodes { get { return _enemyScript.PatrolNodes; } }
        bool _loopRestart { get { return _enemyScript.PatrolLoopRestart; } }

        #region MonoBehaviour

        void Awake()
        {
            _characterScript = GetComponent<CharacterScript>();
            _enemyScript = GetComponent<EnemyScript>();

            ConstructNodes();
        }

        void Start()
        {
            _characterScript.CharacterMovement.OnDestinationReached += CharacterMovement_OnDestinationReached;
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

#if UNITY_EDITOR

        void OnDrawGizmos()
        {
            _enemyScript = GetComponent<EnemyScript>();

            if (_enemyScript.EnemyBehaviour != EnemyScript.Behaviour.Patrol)
                return;

            if (Application.isPlaying)
                return;

            for (int i = 0; i < _nodes.Count; i++)
            {
                if (i > 0)
                    GizmosHelper.DrawLine(CacheTransform.TransformPoint(_nodes[i - 1].Position), CacheTransform.TransformPoint(_nodes[i].Position), Color.yellow);

                GizmosHelper.DrawWireSphere(CacheTransform.TransformPoint(_nodes[i].Position), 0.25f, Color.red);
            }
        }

#endif

        #endregion

        #region IStateMachine

        void IStateMachine.Init()
        {
            this.enabled = false;
        }

        void IStateMachine.OnStart()
        {
            this.enabled = true;

            int closestNode = 0;
            float distance = Mathf.Infinity;

            for (int i = 0; i < _nodes.Count; i++)
            {
                float d = Vector3.Distance(_nodes[i].Position, CacheTransform.position);

                if (d < distance)
                {
                    distance = d;
                    closestNode = i;
                }
            }

            _iNode = closestNode;

            _characterScript.SetMoveSpeed(GameConfig.EnemyWalkSpeed);
            _characterScript.WalkTo(_nodes[_iNode].Position);
        }

        void IStateMachine.OnUpdate()
        {
        }

        void IStateMachine.OnStop()
        {
            _tween?.Kill();

            this.enabled = false;
        }

        #endregion

        void CharacterMovement_OnDestinationReached()
        {
            if (!this.enabled)
                return;

            if (_nodes[_iNode].StopDuration > 0f)
            {
                _characterScript.StopMove();

                _tween?.Kill();
                _tween = DOVirtual.DelayedCall(_nodes[_iNode].StopDuration, MoveNext, false);
            }
            else
            {
                MoveNext();
            }
        }

        void MoveNext()
        {
            _iNode += _isFoward ? 1 : -1;

            if (_iNode < 0 || _iNode >= _nodes.Count)
            {
                if (_loopRestart)
                {
                    _iNode = 0;
                }
                else
                {
                    _isFoward = !_isFoward;
                    _iNode += _isFoward ? 2 : -2;
                }
            }

            MoveToNode();
        }

        void ConstructNodes()
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                _nodes[i].Position = CacheTransform.TransformPoint(_nodes[i].Position);
            }
        }

        void MoveToNode()
        {
            bool success = _characterScript.WalkTo(_nodes[_iNode].Position);

            if (!success)
            {
                _tween?.Kill();
                _tween = DOVirtual.DelayedCall(0.2f, MoveToNode, false);
            }
        }
    }
}