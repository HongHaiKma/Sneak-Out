using PFramework;
using UnityEngine;

namespace Game
{
    public class CharacterFootStep : CacheMonoBehaviour
    {
        [Header("Config")]
        [SerializeField] float _stepDistance;

        float _stepDistanceRemain;
        float _moveSpeed;

        void Start()
        {
            _stepDistanceRemain = _stepDistance;

            PlayerControl playerControl = GetComponent<PlayerControl>();

            playerControl.OnMove += PlayerControl_OnMove; ;
            playerControl.OnStop += PlayerControl_OnStop;
        }

        void PlayerControl_OnStop()
        {
            this.enabled = false;
        }

        void PlayerControl_OnMove(Vector3 dir, float speed)
        {
            _moveSpeed = speed;

            this.enabled = true;
        }

        void Update()
        {
            _stepDistanceRemain -= _moveSpeed * Time.deltaTime;

            if (_stepDistanceRemain <= 0)
            {
                AudioManager.Play(AudioFactory.SfxFootStep);
                _stepDistanceRemain += _stepDistance;
            }
        }
    }
}
