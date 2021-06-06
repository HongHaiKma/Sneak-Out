using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupOutfit : UICanvas
{
    public TextMeshProUGUI txt_CharName;
    private void Awake()
    {
        m_ID = UIID.POPUP_OUTFIT;
        Init();
    }

    public override void StartListenToEvents()
    {
        base.StartListenToEvents();
        EventManagerWithParam<int>.AddListener(GameEvents.LOAD_CHAR_OUTFIT, Event_LOAD_CHAR_OUTFIT);
    }

    public override void StopListenToEvents()
    {
        base.StartListenToEvents();
        EventManagerWithParam<int>.RemoveListener(GameEvents.LOAD_CHAR_OUTFIT, Event_LOAD_CHAR_OUTFIT);
    }

    public void Event_LOAD_CHAR_OUTFIT(int _id)
    {
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(_id);

        txt_CharName.text = config.m_Name;
    }
}
