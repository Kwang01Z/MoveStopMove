using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadSkinDisplay : SkinShop
{
    [SerializeField] HeadSkinData m_HeadSkinData;
    GameObject m_CurrentSkin = null;
    SortedList<int ,GameObject> m_Skins = new SortedList<int, GameObject>();
    int m_SkinIndex;
    private void Start()
    {
        ChangeSkin(0);
    }
    public override int SkinCount()
    {
        return m_HeadSkinData.Skins.Count;
    }
    public override void ChangeSkin(int a_SkinIndex)
    {
        DisableAllSkins();
        if (m_Skins.ContainsKey(a_SkinIndex))
        {
            m_CurrentSkin = m_Skins[a_SkinIndex];
        }
        else
        {
            m_CurrentSkin = m_HeadSkinData.GetSkin(a_SkinIndex, transform, transform.position);
            m_Skins.Add(a_SkinIndex, m_CurrentSkin);
        }
        m_SkinIndex = a_SkinIndex;
        m_CurrentSkin.SetActive(true);
    }
    void DisableAllSkins()
    {
        for (int i = 0; i < m_Skins.Count; i++)
        {
            m_Skins[i]?.SetActive(false);
        }
    }
    public override int SkinPrice()
    {
        return m_HeadSkinData.GetSkinInfo(m_SkinIndex).Price;
    }
    public override string SkinName()
    {
        return m_HeadSkinData.GetSkinInfo(m_SkinIndex).Name;
    }
    public override void SetSkinForChar(CharacterSkin characterSkin)
    {
        characterSkin.SetHeadSkin(m_HeadSkinData.GetSkinInfo(m_SkinIndex).Prefab);
    }
}
