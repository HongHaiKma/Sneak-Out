using PFramework;

namespace Game
{
    public class ButtonSkipLevel : ButtonBase
    {
        static readonly string AdsNotAvailable = "There are no videos available at the moment\nPlease try again later";

        protected override void Button_OnClicked()
        {
            base.Button_OnClicked();

            TimeManager.Pause();

            if (!AdsManager.IsVideoReady())
            {

                PopupHelper.CreateMessage(PrefabFactory.PopupMessage, AdsNotAvailable)
                    .OnClosed += () => { TimeManager.Resume(); };
            }
            else
            {
                AdsManager.ShowVideo((success) =>
                {
                    if (success)
                        SkipLevel();

                    TimeManager.Resume();
                });
            }
        }

        void SkipLevel()
        {
            DataMain.LevelIndex++;

            SceneTransitionManager.ReloadScene();

            _button.interactable = false;
        }
    }
}