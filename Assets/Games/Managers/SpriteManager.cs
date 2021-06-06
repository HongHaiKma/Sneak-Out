using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : Singletons<SpriteManager>
{
    public Sprite[] m_CharCards;
    public Sprite[] m_Miscs;
}

public enum MiscSpriteKeys
{
    UI_CARD_BG_LOCK = 0,
    UI_CARD_BG_UNLOCK = 1,
}
