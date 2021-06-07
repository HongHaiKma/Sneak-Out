using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PopupOutfit : UICanvas
{
    public UICharacterOutfit m_UICharacterOutfit;

    public static int m_SelectedCharacter;
    public TextMeshProUGUI txt_CharName;

    public Button btn_Equip;
    public Button btn_Equipped;
    public Button btn_BuyByGold;
    public Button btn_BuyByAds;

    public Image img_Char;

    public TextMeshProUGUI txt_BuyByGold;
    public TextMeshProUGUI txt_AdsNumber;

    private void Awake()
    {
        m_ID = UIID.POPUP_OUTFIT;
        Init();

        GUIManager.Instance.AddClickEvent(btn_Equip, OnEquip);
        GUIManager.Instance.AddClickEvent(btn_BuyByGold, OnBuyByGold);
        GUIManager.Instance.AddClickEvent(btn_BuyByAds, OnByBuyAdsLogic);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Helper.DebugLog("Gold = " + ProfileManager.GetGold());
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        m_SelectedCharacter = ProfileManager.GetSelectedChar();

        img_Char.sprite = SpriteManager.Instance.m_CharCards[m_SelectedCharacter - 1];

        Event_LOAD_CHAR_OUTFIT(m_SelectedCharacter);
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
        m_SelectedCharacter = _id;
        img_Char.sprite = SpriteManager.Instance.m_CharCards[_id - 1];

        SetClaimBtnLogic(_id);
    }

    public void OnEquip()
    {
        ProfileManager.UnlockNewCharacter(m_SelectedCharacter);
        ProfileManager.SetSelectedCharacter(m_SelectedCharacter);
        EventManagerWithParam<int>.CallEvent(GameEvents.LOAD_CHAR_OUTFIT, m_SelectedCharacter);
        EventManager.CallEvent(GameEvents.UPDATE_OUTFIT);

        GameObject go = PrefabManager.Instance.SpawnCharacter(new Vector3(0f, 0.83333333f, 0f), m_SelectedCharacter);
        Character charr = PlaySceneManager.Instance.m_Char;
        CharacterType type = charr.m_CharacterType;
        PlaySceneManager.Instance.m_Char = go.GetComponent<Character>();
        EventManager.CallEvent(GameEvents.LOAD_CHAR);
        // StartCoroutine(RemoveChar(charr));
        EventManagerWithParam<CharacterType>.CallEvent(GameEvents.REMOVE_CHAR, type);
    }

    public void OnBuyByGold() //Remember to Update UICharacterCard when buy succeed
    {
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_SelectedCharacter);

        if (ProfileManager.IsEnoughGold(config.m_Price))
        // if (ProfileManager.MyProfile.IsEnoughGold(config.m_Price))
        {
            // if (TutorialManager.Instance.CheckTutorial(TutorialType.SHOP_BUYBYGOLD))
            // {
            //     TutorialManager.Instance.PassTutorial(TutorialType.SHOP_BUYBYGOLD);
            //     PopupCaller.GetTutorialPopup().SetupTutShopByBuyGold_UnClickBuyByGoldUI(GetComponent<RectTransform>());
            //     // PopupCaller.GetTutorialPopup().OnClose();
            // }

            // ProfileManager.ConsumeGold(config.m_Price);
            // ProfileManager.UnlockNewCharacter(m_SelectedCharacter);
            // ProfileManager.SetSelectedCharacter(m_SelectedCharacter);
            // SetOwned(m_SelectedCharacter);
            // EventManagerWithParam<int>.CallEvent(GameEvent.CLAIM_CHAR, m_SelectedCharacter);
            // EventManagerWithParam<int>.CallEvent(GameEvent.EQUIP_CHAR, m_SelectedCharacter);

            // txt_TotalGold.text = ProfileManager.GetGold();
            // GameManager.Instance.GetPanelInGame().txt_TotalGold.text = ProfileManager.GetGold();

            // SoundManager.Instance.PlaySoundBuySuccess();

            // AnalysticsManager.LogUnlockCharacter(config.m_Id, config.m_Name);

            ProfileManager.ConsumeGold(config.m_Price);
            OnEquip();
            Helper.DebugLog("Gold: " + ProfileManager.GetGold());
        }
        else
        {
            // StartCoroutine(IEWarning());
            Helper.DebugLog("Not enough golddddddddd");
            Helper.DebugLog("Gold: " + ProfileManager.GetGold());
        }
    }

    public void OnByBuyAdsLogic()
    {
        CharacterProfileData data = ProfileManager.GetCharacterProfileData(m_SelectedCharacter);
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_SelectedCharacter);

        if (data == null)
        {
            ProfileManager.UnlockNewCharacter(m_SelectedCharacter);
            data = new CharacterProfileData();
            data = ProfileManager.GetCharacterProfileData(m_SelectedCharacter);
            // data.ClaimByAds(1);
        }
        // else
        // {
        //     data.ClaimByAds(1);
        // }

        data.ClaimByAds(1);

        if (ProfileManager.IsOwned(m_SelectedCharacter))
        {
            // ProfileManager.SetSelectedCharacter(m_SelectedCharacter);
            // EventManagerWithParam<int>.CallEvent(GameEvent.EQUIP_CHAR, m_SelectedCharacter);

            OnEquip();

            // SoundManager.Instance.PlaySoundBuySuccess();

            // AnalysticsManager.LogUnlockCharacter(config.m_Id, config.m_Name);
        }

        EventManager.CallEvent(GameEvents.UPDATE_OUTFIT);
        Event_LOAD_CHAR_OUTFIT(m_SelectedCharacter);

        // SetOwned(m_SelectedCharacter);
        // EventManagerWithParam<int>.CallEvent(GameEvent.CLAIM_CHAR, m_SelectedCharacter);
    }

    public void SetClaimBtnLogic(int _id)
    {
        CharacterProfileData data = ProfileManager.GetCharacterProfileData(m_SelectedCharacter);
        CharacterDataConfig config = GameData.Instance.GetCharacterDataConfig(m_SelectedCharacter);

        bool _checkowned = ProfileManager.IsOwned(_id);
        bool _adsCheck = config.CheckAds();

        bool equipped = (_id == ProfileManager.GetSelectedCharacter());

        if (equipped)
        {
            btn_Equipped.gameObject.SetActive(equipped);
            btn_BuyByAds.gameObject.SetActive(!equipped);
            btn_BuyByGold.gameObject.SetActive(!equipped);
            btn_Equip.gameObject.SetActive(!equipped);
            return;
        }

        if (_checkowned)
        {
            btn_Equipped.gameObject.SetActive(!_checkowned);
            btn_BuyByAds.gameObject.SetActive(!_checkowned);
            btn_BuyByGold.gameObject.SetActive(!_checkowned);
            btn_Equip.gameObject.SetActive(_checkowned);
            // return;
        }
        else
        {
            if (_adsCheck)
            {
                btn_Equipped.gameObject.SetActive(!_adsCheck);
                btn_BuyByAds.gameObject.SetActive(_adsCheck);
                btn_BuyByGold.gameObject.SetActive(!_adsCheck);
                btn_Equip.gameObject.SetActive(!_adsCheck);

                if (data != null)
                {
                    txt_AdsNumber.text = data.m_AdsNumber.ToString() + "/" + config.m_AdsNumber.ToString();
                }
                else
                {
                    txt_AdsNumber.text = "0" + "/" + config.m_AdsNumber.ToString();
                }
            }
            else
            {
                btn_Equipped.gameObject.SetActive(_adsCheck);
                btn_BuyByAds.gameObject.SetActive(_adsCheck);
                btn_BuyByGold.gameObject.SetActive(!_adsCheck);
                btn_Equip.gameObject.SetActive(_adsCheck);

                txt_BuyByGold.text = config.m_Price.ToString();
            }
        }
    }
}
