using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using PolyAndCode.UI;
using UnityEngine.UI;

/// <summary>
/// Demo controller class for Recyclable Scroll Rect. 
/// A controller class is responsible for providing the scroll rect with datasource. Any class can be a controller class. 
/// The only requirement is to inherit from IRecyclableScrollRectDataSource and implement the interface methods
/// </summary>

//Dummy Data model for demostraion

public class UICharacterOutfit : MonoBehaviour, IRecyclableScrollRectDataSource
{
    // [SerializeField]
    public RecyclableScrollRect _recyclableScrollRect;

    [SerializeField]
    private int _dataLength;

    //Dummy data List
    private List<UICharacterCardInfo> _contactList = new List<UICharacterCardInfo>();

    public RectTransform rect_Content;
    public Transform tf_Content;

    //Recyclable scroll rect's data source must be assigned in Awake.
    private void Awake()
    {
        // InitCell();
        // // MiniCharacterStudio.Instance.SpawnMiniCharacterIdle(ProfileManager.GetSelectedCharacter());
        // _recyclableScrollRect.DataSource = this;
    }

    private void OnEnable()
    {
        // _recyclableScrollRect.Initialize();
        // InitCell();
        // MiniCharacterStudio.Instance.SpawnMiniCharacterIdle(ProfileManager.GetSelectedCharacter());
        _recyclableScrollRect.DataSource = this;
        // Event_TEST_UPDATE_NEW_OUTFIT();
        Event_TEST_UPDATE_NEW_OUTFIT();
        StartListenToEvents();
    }

    private void OnDisable()
    {
        StopListenToEvents();
    }

    private void OnDestroy()
    {
        StopListenToEvents();
    }

    public void StartListenToEvents()
    {
        EventManager.AddListener(GameEvents.TEST_UPDATE_NEW_OUTFIT, Event_TEST_UPDATE_NEW_OUTFIT);
    }

    public void StopListenToEvents()
    {
        EventManager.RemoveListener(GameEvents.TEST_UPDATE_NEW_OUTFIT, Event_TEST_UPDATE_NEW_OUTFIT);
    }

    public void Event_TEST_UPDATE_NEW_OUTFIT()
    {
        // int childs = tf_Content.childCount;
        // Helper.DebugLog("Childs: " + childs);
        // for (int i = 0; i < childs - 1; i++)
        // {
        //     Destroy(transform.GetChild(i).gameObject);
        // }


        foreach (Transform child in tf_Content)
        {
            GameObject.Destroy(child.gameObject);
        }



        // _recyclableScrollRect.ReloadData();
        _recyclableScrollRect.Initialize();
        InitCell();

        // StartCoroutine(DestroyCard());
    }

    // IEnumerator DestroyCard()
    // {
    //     foreach (Transform child in tf_Content)
    //     {
    //         GameObject.Destroy(child.gameObject);
    //     }
    //     yield return new WaitUntil(() => tf_Content.childCount == 0);
    //     _recyclableScrollRect.ReloadData();
    //     _recyclableScrollRect.Initialize();
    //     InitCell();
    // }

    //Initialising _contactList with dummy data 
    public void InitCell()
    {
        if (_contactList != null) _contactList.Clear();

        // Dictionary<int, CharacterDataConfig> characterDataConfig = GameData.Instance.GetCharacterDataConfig();
        // int len = characterDataConfig.Count;
        // // _dataLength = len;

        // Dictionary<int, CharacterDataConfig> charConfig = GameData.Instance.GetCharacterDataConfig();
        // int len = charConfig.Count;

        // for (int i = 0; i < len; i++)
        // {
        //     UICharacterCardInfo obj = new UICharacterCardInfo();
        //     obj.m_Id = charConfig[i + 1].m_Id;
        //     obj.m_Name = charConfig[i + 1].m_Name;
        //     obj.m_Price = charConfig[i + 1].m_Price.ToString();
        //     obj.m_AdsNumber = charConfig[i + 1].m_AdsNumber;
        //     _contactList.Add(obj);
        // }

        List<CharacterProfileData> charConfig = ProfileManager.Instance.GetAllCharacterProfile();
        int len = charConfig.Count;

        for (int i = 0; i < len; i++)
        {
            if (ProfileManager.IsOwned((int)charConfig[i].m_Cid))
            {
                UICharacterCardInfo obj = new UICharacterCardInfo();
                obj.m_Id = (int)charConfig[i].m_Cid;
                obj.m_Name = charConfig[i].m_Name;
                // obj.m_Price = charConfig[i + 1].m_Price.ToString();
                obj.m_AdsNumber = charConfig[i].m_AdsNumber;
                _contactList.Add(obj);

                Helper.DebugLog("Data: " + i);
            }
        }
    }

    #region DATA-SOURCE

    /// <summary>
    /// Data source method. return the list length.
    /// </summary>
    public int GetItemCount()
    {
        return _contactList.Count;
    }

    /// <summary>
    /// Data source method. Called for a cell every time it is recycled.
    /// Implement this method to do the necessary cell configuration.
    /// </summary>
    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as UICharacterCard;
        item.ConfigureCell(_contactList[index], index);
    }

    #endregion
}