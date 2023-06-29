using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PantSkinData", menuName = "ScriptableObject/PantSkinData", order = 3)]
public class PantSkinData : ScriptableObject
{
    [SerializeField] List<PantSkinInfo> m_Skins;
    public List<PantSkinInfo> Skins => m_Skins;
    public PantSkinInfo GetSkinInfo(int a_key)
    {
        PantSkinInfo result = null;
        if (m_Skins[a_key] != null)
        {
            result = m_Skins[a_key];
        }
        return result;
    }
}
[System.Serializable]
public class PantSkinInfo
{
    public string Name;
    public int Price;
    public Material Material;
    public Sprite Sprite;
}

