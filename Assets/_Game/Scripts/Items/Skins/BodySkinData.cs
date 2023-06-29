using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BodySkinData", menuName = "ScriptableObject/BodySkinData", order = 3)]
public class BodySkinData : ScriptableObject
{
    [SerializeField] List<BodySkinInfo> m_Skins;
    public List<BodySkinInfo> Skins => m_Skins;
    public BodySkinInfo GetSkinInfo(string a_name)
    {
        BodySkinInfo result = m_Skins[0];
        m_Skins.ForEach((skin) =>
        {
            if (skin.Name.Equals(a_name))
                result = skin;
        });
        return result;
    }
}
[System.Serializable]
public class BodySkinInfo
{
    public string Name;
    public int Price;
    public Material Material;
}
