using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatUI : MonoBehaviour
{
    public Button btn_ChangeChar;
    public InputField m_InputChar;

    private void Awake()
    {

        GUIManager.Instance.AddClickEvent(btn_ChangeChar, ChangeChar);
        gameObject.SetActive(false);
    }

    public void ChangeChar()
    {
        int index = int.Parse(m_InputChar.text);
        ProfileManager.SetSelectedCharacter(index);
        GameObject go = PrefabManager.Instance.SpawnCharacter(new Vector3(0f, 0.83333333f, 0f), index);
        Character charr = PlaySceneManager.Instance.m_Char;
        CharacterType type = charr.m_CharacterType;
        PlaySceneManager.Instance.m_Char = go.GetComponent<Character>();
        EventManager.CallEvent(GameEvents.LOAD_CHAR);
        // StartCoroutine(RemoveChar(charr));
        EventManagerWithParam<CharacterType>.CallEvent(GameEvents.REMOVE_CHAR, type);
    }

    IEnumerator RemoveChar(Character _charr)
    {
        yield return Yielders.Get(0.5f);
        Destroy(_charr.gameObject);
    }
}
