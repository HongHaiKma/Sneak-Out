using DG.Tweening;
using PFramework;
using UnityEngine;

namespace Game
{
    public class StartGameHandler : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] float _popupDelay = 1f;

        Tween _tween;
        bool _tutorialShown;
        bool _ratingShown;

        #region MonoBehaviour

        void Start()
        {
            ShowStartPopup();
        }

        void OnDestroy()
        {
            _tween?.Kill();
        }

        #endregion

        void ShowStartPopup()
        {
            if (!_tutorialShown && GameHelper.GetCurrentTutorialConfig() != null)
            {
                ShowPopup(PrefabFactory.PopupTutorial);
                _tutorialShown = true;
                return;
            }

            if (!_ratingShown && !DataMain.RateGame)
            {
                if ((DataMain.LevelIndex - GameConfig.RatingStart) % GameConfig.RatingRepeatCount == 0)
                {
                    ShowPopup(PrefabFactory.PopupRating);
                    _ratingShown = true;
                    return;
                }
            }
        }

        void ShowPopup(GameObject objPopup)
        {
            _tween?.Kill();
            _tween = DOVirtual.DelayedCall(_popupDelay, () =>
            {
                TimeManager.Pause();
                PopupHelper.Create(objPopup).OnClosed += Popup_OnClosed;
            }, false);
        }

        void Popup_OnClosed()
        {
            TimeManager.Resume();

            ShowStartPopup();
        }
    }
}