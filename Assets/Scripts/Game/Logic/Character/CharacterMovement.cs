using PFramework;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class CharacterMovement : CacheMonoBehaviour
    {
        [Header("Config")]
        [SerializeField] float _maxSpeed;

        NavMeshAgent _navMeshAgent;
        NavMeshPath _navMeshPath;

        Vector3 _direction;
        float _speedNormalized;
        bool _movingToDestination = false;

        public Vector3 Direction { set { _direction = value; } }
        public float SpeedNormalized
        {
            set
            {
                if (!this.enabled)
                    return;

                _speedNormalized = value;
                OnSpeedChanged?.Invoke(value);
            }
        }

        public event Callback OnDestinationReached;
        public event Callback<float> OnSpeedChanged;

        #region MonoBehaviour

        void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshPath = new NavMeshPath();
        }

        void Update()
        {
            if (_speedNormalized > 0f)
            {
                CacheTransform.forward = _direction;
                _navMeshAgent.Move(_direction * _speedNormalized * _maxSpeed * Time.deltaTime);
            }


            if (_movingToDestination && !_navMeshAgent.pathPending)
            {
                if (_navMeshAgent.remainingDistance <= 0.1f)
                    DestinationReached();
            }
        }

        #endregion

        #region Public

        public void SetPriority(int priorityIndex)
        {
            _navMeshAgent.avoidancePriority = priorityIndex;
        }

        public void SetEnabled(bool enabled)
        {
            if (!enabled)
            {
                SpeedNormalized = 0f;
                _navMeshAgent.isStopped = false;
            }

            _navMeshAgent.enabled = enabled;

            this.enabled = enabled;
        }

        public bool MoveTo(Vector3 destination)
        {
            _navMeshAgent.CalculatePath(destination, _navMeshPath);

            if (_navMeshPath.status != NavMeshPathStatus.PathComplete)
            {
                return false;
            }
            else
            {
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetPath(_navMeshPath);
                _movingToDestination = true;

                return true;
            }
        }

        public void SetSpeed(float speed)
        {
            _navMeshAgent.speed = speed;
        }

        public void Stop()
        {
            _navMeshAgent.isStopped = true;

            _movingToDestination = false;
        }

        #endregion

        void DestinationReached()
        {
            _movingToDestination = false;

            OnDestinationReached?.Invoke();
        }
    }
}