using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    public class EndGameHandler : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] float _winDelay;
        [SerializeField] float _loseDelay;

        Tween _tween;

        #region MonoBehaviour

        void Start()
        {
            Messenger.AddListener<bool>(GameEvent.Game_End, GameEvent_GameEnd);
        }

        void OnDestroy()
        {
            Messenger.RemoveListener<bool>(GameEvent.Game_End, GameEvent_GameEnd);

            _tween?.Kill();
        }

        #endregion

        void GameEvent_GameEnd(bool isWin)
        {
            if (isWin)
            {
                DataMain.LevelIndex++;

                DataMain.LevelMax = Mathf.Max(DataMain.LevelMax, DataMain.LevelIndex);
                DataMain.LevelMax = Mathf.Min(DataMain.LevelMax, GameHelper.TotalLevel - 1);

                Taptic.Taptic.Success();

                AudioManager.Play(AudioFactory.SfxSuccess);
            }
            else
            {
                Taptic.Taptic.Vibrate();
            }

            ShowEndGamePopup(isWin);
        }

        void ShowEndGamePopup(bool isWin)
        {
            float duration = isWin ? _winDelay : _loseDelay;
            GameObject objPopup = isWin ? PrefabFactory.PopupWin : PrefabFactory.PopupLose;

            _tween = DOVirtual.DelayedCall(isWin ? _winDelay : _loseDelay, () =>
            {
                AdsShowHandler.Instance.Show(() =>
                {
                    PopupHelper.Create(objPopup);
                });
            });
        }
    }
}