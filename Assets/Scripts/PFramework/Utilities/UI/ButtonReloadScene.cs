namespace PFramework
{
    public class ButtonReloadScene : ButtonBase
    {
        protected override void Button_OnClicked()
        {
            base.Button_OnClicked();



            SceneTransitionManager.ReloadScene();
        }
    }
}