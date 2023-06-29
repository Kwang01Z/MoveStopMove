using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinShopDisplay : MonoBehaviour
{
    [SerializeField] List<CharSkinData> m_SkinDatas;
    SkinType m_CurrentSkinType = SkinType.HeadSkin;
    public SkinType CurrentSkinType => m_CurrentSkinType;
    int m_CurrentSkinIndex = 0;

    public void ChangeSkinType(SkinType a_SkinType)
    {
        if (!m_CurrentSkinType.Equals(a_SkinType))
        {
            m_CurrentSkinType = a_SkinType;
            DisplayShopByType(m_CurrentSkinType);
            m_CurrentSkinIndex = 0; 
            GetShopByType(m_CurrentSkinType).ChangeSkin(0);
        }
    }
    public void ChangeSkinIndex(int a_IndexChange)
    {
        if (m_CurrentSkinIndex + a_IndexChange > GetShopByType(m_CurrentSkinType).SkinCount() - 1)
        {
            m_CurrentSkinIndex = 0;
        }
        else if (m_CurrentSkinIndex + a_IndexChange < 0)
        {
            m_CurrentSkinIndex = GetShopByType(m_CurrentSkinType).SkinCount() - 1;
        }
        else
        {
            m_CurrentSkinIndex += a_IndexChange;
        }
        GetShopByType(m_CurrentSkinType).ChangeSkin(m_CurrentSkinIndex);
    }
    SkinShop GetShopByType(SkinType type)
    {
        SkinShop result = null;
        m_SkinDatas.ForEach((shop)=>
        {
            if (shop.Type.Equals(type)) result = shop.Shop;
        });
        return result;
    }
    void DisplayShopByType(SkinType a_Type)
    {
        m_SkinDatas.ForEach((shop)=>
        {
            shop.Shop.gameObject.SetActive(shop.Type.Equals(a_Type));
        });
    }
    public int GetCurrentItemPrice()
    {
        return GetShopByType(m_CurrentSkinType).SkinPrice();
    }
    public string GetCurrentItemName()
    {
        return GetShopByType(m_CurrentSkinType).SkinName();
    }
    public void SetSkinForChar(CharacterSkin characterSkin)
    {
        GetShopByType(m_CurrentSkinType).SetSkinForChar(characterSkin);
    }
}
[System.Serializable]
public class CharSkinData
{
    public SkinType Type;
    public SkinShop Shop;
}
public enum SkinType
{
    HeadSkin,
    Pant,
    Body,
    Clother
}


