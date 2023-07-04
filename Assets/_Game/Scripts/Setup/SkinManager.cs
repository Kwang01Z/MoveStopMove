using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : Singleton<SkinManager>
{
    [SerializeField] SkinData<HeadSkinInfo> m_HeadSkinData;
    [SerializeField] SkinData<PantSkinInfo> m_PantSkinData;
    [SerializeField] SkinData<ShieldInfo> m_ShieldData;
    [SerializeField] SkinData<SetFullInfo> m_SetFullData;

    public GameObject GetHeadSkin(string a_name, Transform a_parent)
    {
        HeadSkinInfo result = m_HeadSkinData.GetSkin(a_name);
        GameObject obj =  Instantiate(result.Prefab, a_parent);
        return obj;
    }
    public HeadSkinInfo GetHeadSkinInfo(string a_name)
    { 
        return m_HeadSkinData.GetSkin(a_name);
    }
    public string GetRandomHeadSkinTxt()
    {
        return m_HeadSkinData.GetRandomSkin();
    }
    public string GetRandomPantSkinTxt()
    {
        return m_PantSkinData.GetRandomSkin();
    }
    public PantSkinInfo GetPantSkin(string a_name)
    {
        return m_PantSkinData.GetSkin(a_name);
    }
    public List<string> GetListHeadSkin()
    {
        return m_HeadSkinData.GetListSkin();
    }
    public List<string> GetListPantSkin()
    {
        return m_PantSkinData.GetListSkin();
    }
    public List<string> GetListSetFull()
    {
        return m_SetFullData.GetListSkin();
    }
    public SetFullInfo GetSetFull(string a_name)
    {
        return m_SetFullData.GetSkin(a_name);
    }
}