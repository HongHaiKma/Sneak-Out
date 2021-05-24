using PFramework;

namespace Game
{
    public class ButtonPause : ButtonBase
    {
        protected override void Button_OnClicked()
        {
            base.Button_OnClicked();

            TimeManager.Pause();

            PopupHelper.Create(PrefabFactory.PopupPause).OnClosed += PopupPause_OnClosed;

            CacheGameObject.SetActive(false);
        }

        void PopupPause_OnClosed()
        {
            TimeManager.Resume();

            CacheGameObject.SetActive(true);
        }
    }
}