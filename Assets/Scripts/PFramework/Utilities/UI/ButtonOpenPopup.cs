using UnityEngine;

namespace PFramework
{
    public class ButtonOpenPopup : ButtonBase
    {
        [SerializeField] GameObject _popup;

        protected override void Button_OnClicked()
        {
            base.Button_OnClicked();

            PopupHelper.Create(_popup);
        }
    }
}