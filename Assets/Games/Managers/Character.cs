using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PFramework;

public class Character : CacheMonoBehaviour
{
    public CharacterType m_CharacterType;
    public Game.PlayerControl m_PlayerControl;

    private void OnEnable()
    {
        PlaySceneManager.Instance.m_Char = this;
        EventManager.CallEvent(GameEvents.LOAD_CHAR);
        EventManagerWithParam<CharacterType>.AddListener(GameEvents.REMOVE_CHAR, Remove);
    }

    private void OnDisable()
    {
        EventManagerWithParam<CharacterType>.RemoveListener(GameEvents.REMOVE_CHAR, Remove);
    }

    private void OnDestroy()
    {
        EventManagerWithParam<CharacterType>.RemoveListener(GameEvents.REMOVE_CHAR, Remove);
    }

    public void Remove(CharacterType _type)
    {
        if (_type == m_CharacterType)
        {
            Destroy(gameObject);
        }
    }
}
