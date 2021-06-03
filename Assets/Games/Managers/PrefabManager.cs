using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : Singletons<PrefabManager>
{
    public GameObject[] m_CharacterPrefabs;

    private void Awake()
    {
        InitPrefab();
        InitIngamePrefab();
    }

    public void InitPrefab()
    {

    }

    public void InitIngamePrefab()
    {

    }

    public void CreatePool(string name, GameObject prefab, int amount)
    {
        SimplePool.Preload(prefab, amount, name);
    }


}
