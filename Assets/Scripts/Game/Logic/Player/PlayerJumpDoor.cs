using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    public class PlayerJumpDoor : CacheMonoBehaviour, IStateMachine
    {
        [Header("Config")]
        [SerializeField] float _jumpPower;
        [SerializeField] float _inDuration;
        [SerializeField] Ease _inEase;
        [SerializeField] float _outDuration;
        [SerializeField] Ease _outEase;
        [SerializeField] float _depth;
        [SerializeField] float _teleDuration;

        PlayerScript _playerScript;
        CharacterMovement _characterMovement;
        CharacterRenderer _characterRenderer;

        DoorJump _dvStart, _dvEnd;
        bool _readyJumpOut = false;
        GameObject _objDoorJumpTip;

        Tween _tween;

        public event Callback OnJumpOutComplete;

        #region MonoBehaviour

        void Awake()
        {
            _playerScript = GetComponent<PlayerScript>();
            _characterMovement = GetComponent<CharacterMovement>();
            _characterRenderer = GetComponent<CharacterRenderer>();

            PlayerControl playerControl = GetComponent<PlayerControl>();
            playerControl.OnMove += PlayerControl_OnMove;
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        #endregion

        #region Public

        public void Construct(DoorJump start, DoorJump end)
        {
            _dvStart = start;
            _dvEnd = end;
        }

        #endregion

        #region IStateMachine

        void IStateMachine.Init()
        {
        }

        void IStateMachine.OnStart()
        {
            _readyJumpOut = false;

            _playerScript.IsHiding = true;

            _characterMovement.SetEnabled(false);
            _characterRenderer.PlayJump();

            _dvStart.SetEnabled(false);
            _dvEnd.SetEnabled(false);

            _tween = CacheTransform.DOJump(_dvStart.Position + Vector3.down * _depth, _jumpPower + _depth, 1, _inDuration)
                .SetEase(_inEase)
                .OnComplete(Tween_OnJumpInComplete);

            AudioManager.Play(AudioFactory.SfxJump);
        }

        void IStateMachine.OnUpdate()
        {
        }

        void IStateMachine.OnStop()
        {
        }

        #endregion

        void PlayerControl_OnMove(Vector3 dir, float speed)
        {
            if (!_readyJumpOut)
                return;

            _readyJumpOut = false;

            JumpOut();
        }

        void Tween_OnJumpInComplete()
        {
            _characterRenderer.SetVisible(false);

            _tween = CacheTransform.DOMove(_dvEnd.Position + Vector3.down * _depth, _teleDuration)
                .OnComplete(Tween_OnTeleComplete);
        }

        void Tween_OnTeleComplete()
        {
            if (_objDoorJumpTip == null)
                _objDoorJumpTip = PrefabFactory.ObjDoorJumpTips.Create();

            _objDoorJumpTip.gameObject.SetActive(true);
            _objDoorJumpTip.transform.SetXZ(CacheTransform.position.x, CacheTransform.position.z);

            _readyJumpOut = true;
        }

        void JumpOut()
        {
            _objDoorJumpTip.SetActive(false);

            _characterRenderer.SetVisible(true);
            _characterRenderer.PlayJump();

            _dvEnd.PlayOpen();
            _dvStart.SetEnabled(true);

            _tween = CacheTransform.DOJump(_dvEnd.Position, _jumpPower, 1, _outDuration)
                .SetEase(_outEase)
                .OnComplete(Tween_OnJumpOutComplete);

            AudioManager.Play(AudioFactory.SfxJump);
        }

        void Tween_OnJumpOutComplete()
        {
            _playerScript.IsHiding = false;

            _characterMovement.SetEnabled(true);

            OnJumpOutComplete?.Invoke();
        }
    }
}