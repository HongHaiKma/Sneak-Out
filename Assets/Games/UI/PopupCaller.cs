using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupCaller : Singletons<PopupCaller>
{
    public static void OpenOutfitPopup()
    {
        PopupOutfit popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_OUTFIT) as PopupOutfit;

        GUIManager.Instance.ShowUIPopup(popup);
    }
}
