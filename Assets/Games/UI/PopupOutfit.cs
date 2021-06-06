using UnityEngine.UI;
using TMPro;

public class PopupOutfit : UICanvas
{
    public static int m_SelectedCharacter;
    public TextMeshProUGUI txt_CharName;

    public Button btn_Equip;
    public Button btn_Equipped;
    public Button btn_BuyByGold;
    public Button btn_BuyByAds;

    public TextMeshProUGUI txt_BuyByGold;
    public TextMeshProUGUI txt_AdsNumber;

    private void Awake()
    {
        m_ID = UIID.POPUP_OUTFIT;
        Init();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        m_SelectedCharacter = ProfileManager.GetSelectedChar();
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

        SetClaimBtnLogic(_id);
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
