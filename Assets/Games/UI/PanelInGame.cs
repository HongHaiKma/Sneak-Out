using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInGame : MonoBehaviour
{
    public Button btn_Outfit;

    private void Awake()
    {
        GUIManager.Instance.AddClickEvent(btn_Outfit, OpenOutfit);
    }

    private void OnEnable()
    {
        btn_Outfit.gameObject.SetActive(true);
    }

    public void OpenOutfit()
    {
        PopupCaller.OpenOutfitPopup();
    }
}
