using PFramework;
using UnityEngine;

namespace Game
{
    [DefaultExecutionOrder(-93)]
    public class Follower : CacheMonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] Transform _tfTarget;

        [Header("Config")]
        [SerializeField] bool _lockX;
        [SerializeField] bool _lockY;
        [SerializeField] bool _lockZ;

        private void Awake()
        {
            EventManager.AddListener(GameEvents.LOAD_CHAR, Event_LOAD_CHAR);
        }

        private void OnEnable()
        {
            // EventManager.AddListener(GameEvents.LOAD_CHAR, Event_LOAD_CHAR);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(GameEvents.LOAD_CHAR, Event_LOAD_CHAR);
        }

        private void OnDestroy()
        {
            EventManager.RemoveListener(GameEvents.LOAD_CHAR, Event_LOAD_CHAR);
        }

        public void Event_LOAD_CHAR()
        {
            _tfTarget = PlaySceneManager.Instance.m_Char.CacheTransform;
        }

        void LateUpdate()
        {
            CacheTransform.position = new Vector3(
                _lockX ? CacheTransform.position.x : _tfTarget.position.x,
                _lockY ? CacheTransform.position.y : _tfTarget.position.y,
                _lockZ ? CacheTransform.position.z : _tfTarget.position.z);
        }
    }
}