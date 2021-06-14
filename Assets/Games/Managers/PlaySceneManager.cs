using UnityEngine.UI;
using Game;
using PFramework;
using TMPro;
using UnityEngine;

public class PlaySceneManager : Singletons<PlaySceneManager>
{
    public Character m_Char;
    public Button btn_SkipLevel;
    public TextMeshProUGUI txt_Gold;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ProfileManager.AddGold(100);
            EventManager.CallEvent(GameEvents.UPDATE_GOLD);
        }
    }

    private void Awake()
    {
        GUIManager.Instance.AddClickEvent(btn_SkipLevel, OnSkipLevel);

        Event_UPDATE_GOLD();
    }

    private void OnEnable()
    {
        EventManager.AddListener(GameEvents.SKIP_LEVEL, SkipLevel);
        EventManager.AddListener(GameEvents.UPDATE_GOLD, Event_UPDATE_GOLD);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(GameEvents.SKIP_LEVEL, SkipLevel);
        EventManager.RemoveListener(GameEvents.UPDATE_GOLD, Event_UPDATE_GOLD);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(GameEvents.SKIP_LEVEL, SkipLevel);
        EventManager.RemoveListener(GameEvents.UPDATE_GOLD, Event_UPDATE_GOLD);
    }

    public void OnSkipLevel()
    {
        AdsManagers.Instance.WatchRewardVideo(RewardType.SKIP_LEVEL);
    }

    public void Event_UPDATE_GOLD()
    {
        txt_Gold.text = ProfileManager.GetGold();
    }

    public void SkipLevel()
    {
        DataMain.LevelIndex++;

        SceneTransitionManager.ReloadScene();

        // _button.interactable = false;
    }
}
