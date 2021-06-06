using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;
using TMPro;

//Cell class for demo. A cell in Recyclable Scroll Rect must have a cell class inheriting from ICell.
//The class is required to configure the cell(updating UI elements etc) according to the data during recycling of cells.
//The configuration of a cell is done through the DataSource SetCellData method.
//Check RecyclableScrollerDemo class
public class UICharacterCard : MonoBehaviour, ICell
{
    //UI
    public TextMeshProUGUI m_Name;
    public Button btn_LoadChar;
    // public Image img_Char;
    // public Image img_BG;
    // public Text txt_Price;
    // public Text txt_AdsClaim;

    // public GameObject g_SelectedOutline;
    // public GameObject g_Owned;
    // public GameObject g_Equipped;
    // public GameObject g_Price;
    // public GameObject g_AdsClaim;
    // public GameObject g_Lock;

    //Model
    private UICharacterCardInfo m_UICharacterCardInfo;
    private int _cellIndex;

    // public int txt_SelectChar;

    private void Awake()
    {
        // img_Char.sprite = SpriteManager.Instance.m_CharCards[m_UIChar];
        // Helper.DebugLog("UICharacterCard Starttttttttttttttttttttttttttt");
        GUIManager.Instance.AddClickEvent(btn_LoadChar, Event_LOAD_CHAR);
    }

    // private void OnEnable()
    // {
    //     // g_SelectedOutline.SetActive(ProfileManager.CheckSelectedChar(m_UICharacterCardInfo.m_Id));

    //     StartListenToEvent();
    // }

    // private void OnDisable()
    // {
    //     StopListenToEvent();
    // }

    // private void OnDestroy()
    // {
    //     StopListenToEvent();
    // }

    // public void StartListenToEvent()
    // {
    //     EventManagerWithParam<int>.AddListener(GameEvents.EQUIP_CHAR, SetEquippedChar);
    //     EventManagerWithParam<int>.AddListener(GameEvents.CLAIM_CHAR, OnUpdateAdsNumber);
    //     EventManager.AddListener(GameEvents.UI_CARD_SET_SELECT_CHAR, OnSetSelectedCharacter);
    //     Helper.DebugLog("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
    // }

    // public void StopListenToEvent()
    // {
    //     EventManagerWithParam<int>.RemoveListener(GameEvents.EQUIP_CHAR, SetEquippedChar);
    //     EventManagerWithParam<int>.RemoveListener(GameEvents.CLAIM_CHAR, OnUpdateAdsNumber);
    //     EventManager.RemoveListener(GameEvents.UI_CARD_SET_SELECT_CHAR, OnSetSelectedCharacter);
    //     Helper.DebugLog("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb");
    // }

    // //This is called from the SetCell method in DataSource
    public void ConfigureCell(UICharacterCardInfo _info, int cellIndex)
    {
        _cellIndex = cellIndex;
        m_UICharacterCardInfo = _info;

        // CharacterProfileData data = ProfileManager.GetCharacterProfileData(_info.m_Id);

        // if (data != null)
        // {
        //     txt_AdsClaim.text = data.m_AdsNumber.ToString() + "/" + _info.m_AdsNumber.ToString();
        // }
        // else
        // {
        //     txt_AdsClaim.text = "0" + "/" + _info.m_AdsNumber.ToString();
        // }

        m_Name.text = _info.m_Name;
        // txt_Price.text = _info.m_Price;

        // img_Char.sprite = SpriteManager.Instance.m_CharCards[_info.m_Id - 1];
        // // img_Char.sprite = SpriteManager.Instance.m_CharCard.GetSprite(_info.m_Name);

        // PopupOutfit popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_OUTFIT) as PopupOutfit;
        // g_SelectedOutline.SetActive(_info.m_Id == popup.m_SelectedCharacter);

        // SetCellStatus();
    }

    // public bool CheckTutorial()
    // {
    //     if (TutorialManager.Instance.CheckTutorial(TutorialType.SHOP_BUYBYGOLD))
    //     {
    //         return true;
    //     }
    //     return false;
    // }

    // public void SetCellStatus()
    // {
    //     CharacterProfileData data = ProfileManager.GetCharacterProfileData(m_UICharacterCardInfo.m_Id);
    //     CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_UICharacterCardInfo.m_Id);
    //     txt_SelectChar = m_UICharacterCardInfo.m_Id;
    //     if (ProfileManager.IsOwned(m_UICharacterCardInfo.m_Id))
    //     {
    //         if (ProfileManager.CheckSelectedChar(m_UICharacterCardInfo.m_Id))
    //         {
    //             g_Equipped.SetActive(true);
    //             g_Owned.SetActive(false);
    //         }
    //         else
    //         {
    //             g_Equipped.SetActive(false);
    //             g_Owned.SetActive(true);
    //         }

    //         g_Price.SetActive(false);
    //         g_AdsClaim.SetActive(false);
    //         g_Lock.SetActive(false);
    //         img_BG.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_CARD_BG];
    //     }
    //     else
    //     {
    //         g_Owned.SetActive(false);
    //         g_Equipped.SetActive(false);
    //         g_Lock.SetActive(true);

    //         bool adsCheck;

    //         if (config.m_AdsCheck == 1)
    //         {
    //             adsCheck = true;
    //         }
    //         else
    //         {
    //             adsCheck = false;
    //         }

    //         g_Price.SetActive(!adsCheck);
    //         g_AdsClaim.SetActive(adsCheck);
    //         img_BG.sprite = SpriteManager.Instance.m_Mics[(int)MiscSpriteKeys.UI_CARD_BG_LOCK];
    //     }

    //     if (m_UICharacterCardInfo.m_Id == 2)
    //     {
    //         // if (CheckTutorial())
    //         if (TutorialManager.Instance.CheckTutorial(TutorialType.SHOP_BUYBYGOLD))
    //         {
    //             PopupCaller.OpenTutorialPopup(false);
    //             PopupCaller.GetTutorialPopup().SetupTutShopByBuyGold_ClickCharUI(GetComponent<RectTransform>());
    //         }
    //     }
    // }

    private void Event_LOAD_CHAR()
    {
        // // MiniCharacterStudio.Instance.SetChar(m_UICharacterCardInfo.m_Id);
        EventManagerWithParam<int>.CallEvent(GameEvents.LOAD_CHAR_OUTFIT, m_UICharacterCardInfo.m_Id);
        // EventManager.CallEvent(GameEvent.UI_CARD_SET_SELECT_CHAR);
        // // PopupOutfit popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_OUTFIT) as PopupOutfit;
        // // g_SelectedOutline.SetActive(m_UICharacterCardInfo.m_Id == popup.m_SelectedCharacter);

        // // if (CheckTutorial.TutorialType.)
        // // {

        // // }

        // if (m_UICharacterCardInfo.m_Id == 2)
        // {
        //     if (CheckTutorial())
        //     {
        //         PopupCaller.GetTutorialPopup().SetupTutShopByBuyGold_UnClickCharUI(GetComponent<RectTransform>());
        //     }
        // }
    }

    // private void OnSetSelectedCharacter()
    // {
    //     PopupOutfit popup = GUIManager.Instance.GetUICanvasByID(UIID.POPUP_OUTFIT) as PopupOutfit;
    //     g_SelectedOutline.SetActive(m_UICharacterCardInfo.m_Id == popup.m_SelectedCharacter);
    //     // g_SelectedOutline.SetActive(true);
    // }

    // public void OnUpdateAdsNumber(int _id)
    // {
    //     if (_id == m_UICharacterCardInfo.m_Id)
    //     {
    //         SetCellStatus();

    //         SetEquippedChar(_id);

    //         CharacterProfileData data = ProfileManager.GetCharacterProfileData(_id);
    //         CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(_id);

    //         txt_AdsClaim.text = data.m_AdsNumber.ToString() + "/" + config.m_AdsNumber.ToString();
    //     }
    //     // else
    //     // {
    //     //     g_SelectedOutline.SetActive(false);
    //     // }
    // }

    // public void SetEquippedChar(int _id)
    // {
    //     CharacterProfileData data = ProfileManager.GetCharacterProfileData(m_UICharacterCardInfo.m_Id);
    //     CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_UICharacterCardInfo.m_Id);

    //     g_SelectedOutline.SetActive(ProfileManager.CheckSelectedChar(m_UICharacterCardInfo.m_Id));

    //     SetCellStatus();
    // }
}

public class UICharacterCardInfo
{
    public int m_Id;
    public string m_Name;
    public string m_Price;
    public int m_AdsNumber;
}