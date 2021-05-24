using UnityEngine;

namespace Game
{
    public class CharacterRenderer : MonoBehaviour
    {
        Animator _animator;
        int _lastTriggerID = 0;

        #region MonoBehaviour

        void Awake()
        {
            _animator = GetComponentInChildren<Animator>();

            GetComponent<CharacterMovement>().OnSpeedChanged += CharacterMovement_OnSpeedChanged;
        }

        #endregion

        #region Public

        public void SetVisible(bool enabled)
        {
            _animator.gameObject.SetActive(enabled);
        }

        public void SetEnabled(bool enabled)
        {
            _animator.enabled = enabled;
        }

        public void SetSpeed(float speed)
        {
            _animator.speed = speed;
        }

        public void PlayAttack()
        {
            SetTrigger(HashDictionary.Attack);
        }

        public void PlayJump()
        {
            _animator.Play(HashDictionary.Jump);
        }

        public void PlayIdle()
        {
            SetTrigger(HashDictionary.Idle);
        }

        public void PlayWalk()
        {
            SetTrigger(HashDictionary.Walk);
        }

        public void PlayRun()
        {
            SetTrigger(HashDictionary.Run);
        }

        public void PlaySleep()
        {
            SetTrigger(HashDictionary.Sleep);
        }

        public void PlayAwake()
        {
            SetTrigger(HashDictionary.Awake);
        }

        public void PlayVictory()
        {
            _animator.Play(HashDictionary.Victory);
        }

        #endregion

        void CharacterMovement_OnSpeedChanged(float speedNormalized)
        {
            SetMoveSpeed(speedNormalized);
        }

        void SetMoveSpeed(float speed)
        {
            _animator.SetFloat(HashDictionary.Speed, speed);
        }

        void SetTrigger(int triggerID)
        {
            if (_lastTriggerID != 0)
                _animator.ResetTrigger(_lastTriggerID);

            _lastTriggerID = triggerID;

            _animator.SetTrigger(triggerID);
        }
    }
}