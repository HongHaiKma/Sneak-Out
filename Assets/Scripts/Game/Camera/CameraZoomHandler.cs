using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    [DefaultExecutionOrder(-92)]
    public class CameraZoomHandler : MonoBehaviour
    {
        enum State
        {
            Normal,
            Overview,
            Sneak,
        }

        [Header("Reference")]
        [SerializeField] PlayerControl _playerControl;

        [Header("Config")]
        [SerializeField] float _overviewDelay;
        [SerializeField] float _overviewScale;
        [SerializeField] float _sneakScale;
        [SerializeField] float _victoryScale;

        [SerializeField] float _overviewDuration;
        [SerializeField] float _normalDuration;
        [SerializeField] float _sneakDuration;
        [SerializeField] float _victoryDuration;

        CameraZoom _cameraZoom;
        State _state = State.Normal;
        Tween _tween;

        #region MonoBehaviour

        void Awake()
        {
            _cameraZoom = GetComponent<CameraZoom>();

            EventManager.AddListener(GameEvents.LOAD_CHAR, Event_LOAD_CHAR);

            // _playerControl.OnMove += PlayerControl_OnMove;
            // _playerControl.OnStop += PlayerControl_OnStop;
        }

        void Start()
        {
            Messenger.AddListener<bool>(GameEvent.Game_End, GameEvent_GameEnd);
        }

        private void OnEnable()
        {
            // EventManager.AddListener(GameEvents.LOAD_CHAR, Event_LOAD_CHAR);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener(GameEvents.LOAD_CHAR, Event_LOAD_CHAR);
        }

        void OnDestroy()
        {
            EventManager.RemoveListener(GameEvents.LOAD_CHAR, Event_LOAD_CHAR);

            Messenger.RemoveListener<bool>(GameEvent.Game_End, GameEvent_GameEnd);

            _tween?.Kill();
        }

        public void Event_LOAD_CHAR()
        {
            _playerControl = PlaySceneManager.Instance.m_Char.m_PlayerControl;
            _playerControl.OnMove += PlayerControl_OnMove;
            _playerControl.OnStop += PlayerControl_OnStop;
        }

        #endregion

        void GameEvent_GameEnd(bool isWin)
        {
            Messenger.Broadcast(GameEvent.Player_Sneak, false);

            _cameraZoom.Zoom(_victoryScale, _victoryDuration);

            this.enabled = false;
        }

        void PlayerControl_OnStop()
        {
            ChangeState(State.Normal);

            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(_overviewDelay, () => { ChangeState(State.Overview); }, false);
        }

        void PlayerControl_OnMove(Vector3 dir, float speedNormalized)
        {
            _tween?.Kill();

            if (speedNormalized < 0.6f)
                ChangeState(State.Sneak);
            else
                ChangeState(State.Normal);
        }

        void ChangeState(State state)
        {
            if (!this.enabled || state == _state)
                return;

            _state = state;

            switch (state)
            {
                case State.Normal:
                    _cameraZoom.Zoom(1f, _normalDuration);
                    Messenger.Broadcast(GameEvent.Player_Sneak, false);
                    break;
                case State.Sneak:
                    _cameraZoom.Zoom(_sneakScale, _sneakDuration);
                    Messenger.Broadcast(GameEvent.Player_Sneak, true);
                    break;
                case State.Overview:
                    _cameraZoom.Zoom(_overviewScale, _overviewDuration)
                        .SetEase(Ease.OutSine);
                    break;
            }
        }
    }
}