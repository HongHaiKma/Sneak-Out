using UnityEngine;

namespace PFramework
{
    public class ButtonLoadScene : ButtonBase
    {
        [SerializeField] int _sceneIndex;

        protected override void Button_OnClicked()
        {
            base.Button_OnClicked();

            SceneTransitionManager.LoadScene(_sceneIndex);
        }
    }
}