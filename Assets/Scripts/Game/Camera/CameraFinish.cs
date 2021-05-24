using PFramework;
using UnityEngine;

namespace Game
{
    public class CameraFinish : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] float _zoomScale;
        [SerializeField] float _zoomDuration;

        CameraZoom _cameraZoom;

        void Awake()
        {
            _cameraZoom = GetComponent<CameraZoom>();
        }

        void Start()
        {
            Messenger.AddListener<bool>(GameEvent.Game_End, GameEvent_GameEnd);
        }

        void OnDestroy()
        {
            Messenger.RemoveListener<bool>(GameEvent.Game_End, GameEvent_GameEnd);
        }

        void GameEvent_GameEnd(bool isWin)
        {
            _cameraZoom.Zoom(_zoomScale, 0.5f);
        }
    }
}