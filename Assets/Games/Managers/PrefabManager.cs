using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-91)]
public class PrefabManager : Singletons<PrefabManager>
{
    public GameObject[] m_CharPrefabs;

    public GameObject m_Plane;

    private void Awake()
    {
        InitPrefab();
        InitIngamePrefab();
    }

    private void OnEnable()
    {
        SpawnCharacter(new Vector3(0f, 0.833333f, 0f), ProfileManager.GetSelectedChar());
    }

    public void InitPrefab()
    {

    }

    public void InitIngamePrefab()
    {

    }

    public GameObject SpawnCharacter(Vector3 _pos, int _index)
    {
        return Instantiate(m_CharPrefabs[_index - 1], _pos, Quaternion.identity);
    }

    public GameObject SpawnPlane(Vector3 _pos)
    {
        return Instantiate(m_Plane, _pos, Quaternion.identity);
    }

    public void CreatePool(string name, GameObject prefab, int amount)
    {
        SimplePool.Preload(prefab, amount, name);
    }
}
