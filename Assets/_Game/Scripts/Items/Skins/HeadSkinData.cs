using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HeadSkinData",menuName = "ScriptableObject/HeadSkinData",order = 2)]
public class HeadSkinData : ScriptableObject
{
    [SerializeField] List<HeadSkinInfo> m_Skins;
    public List<HeadSkinInfo> Skins => m_Skins;
    public HeadSkinInfo GetSkinInfo(int a_name)
    {
        HeadSkinInfo result = null;
        if (m_Skins[a_name] != null)
        {
            result = m_Skins[a_name];
        }
        return result;
    }
    public GameObject GetSkin(int a_index,Transform a_parent, Vector3 a_posion)
    {
        GameObject result = null;
        if (m_Skins[a_index] != null)
        {
            result = Instantiate(m_Skins[a_index].Prefab,a_parent);
            result.transform.position = a_posion;
        }
        return result;
    }
}

[System.Serializable]
public class HeadSkinInfo
{
    public string Name;
    public int Price;
    public GameObject Prefab;
}