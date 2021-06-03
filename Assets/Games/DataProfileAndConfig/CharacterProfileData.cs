using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProfileData
{
    public CharacterType m_Cid;
    public string m_Name;
    public float m_RunSpeed;
    public int m_AdsNumber;

    public void Init(CharacterType _id)
    {
        m_Cid = _id;
        m_AdsNumber = 0;
    }

    public void Load()
    {
        CharacterDataConfig cdc = GameData.Instance.GetCharacterDataConfig(m_Cid);
        m_Name = cdc.m_Name;
        m_RunSpeed = cdc.m_RunSpeed;
    }

    public void ClaimByAds(int _value)
    {
        m_AdsNumber += _value;
        ProfileManager.Instance.SaveData();
    }
}

public class CharacterDataConfig
{
    public int m_Id;
    public string m_Name;
    public float m_RunSpeed;
    public BigNumber m_Price;
    public int m_AdsCheck;
    public int m_AdsNumber;

    public void Init(int _id, string _name, float _runSpeed, BigNumber _price, int _adsCheck, int _adsNumber)
    {
        m_Id = _id;
        m_Name = _name;
        m_RunSpeed = _runSpeed;
        m_Price = _price;
        m_AdsCheck = _adsCheck;
        m_AdsNumber = _adsNumber;
    }

    public bool CheckAds()
    {
        if (m_AdsCheck == 1)
        {
            return true;
        }

        return false;
    }
}

public enum CharacterType
{
    WHITEMAN = 1,
    AGENT47 = 2,
    ASTRONAUS = 3,
    BLACKNINJA = 4,
    CAPTAIN = 5,
    FLASH = 6,
    GREENNINJA = 7,
    MUAYFIGHTER = 8,
    PIRATE = 9,
}