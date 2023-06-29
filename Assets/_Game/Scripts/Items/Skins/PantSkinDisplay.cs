using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PantSkinDisplay : SkinShop
{
    [SerializeField] PantSkinData m_Data;
    [SerializeField] Image m_Image;
    Sprite m_CurrentSprite;
    int m_SkinIndex = -1;
    SortedList<int, Sprite> m_SpritePool = new SortedList<int, Sprite>();
    private void Start()
    {
        ChangeSkin(0);
    }
    public override void ChangeSkin(int a_SkinIndex)
    {
        if (m_SpritePool.ContainsKey(a_SkinIndex))
        {
            m_CurrentSprite = m_SpritePool[a_SkinIndex];
        }
        else
        {
            m_CurrentSprite = m_Data.GetSkinInfo(a_SkinIndex).Sprite;
            m_SpritePool.Add(a_SkinIndex, m_CurrentSprite);
        }
        m_SkinIndex = a_SkinIndex;
        SetImage(m_CurrentSprite);
    }
    public override int SkinCount()
    {
        return m_Data.Skins.Count;
    }
    void SetImage(Sprite a_sprite)
    {
        m_Image.sprite = a_sprite;
    }
    public override int SkinPrice()
    {
        return m_Data.GetSkinInfo(m_SkinIndex).Price;
    }
    public override string SkinName()
    {
        return m_Data.GetSkinInfo(m_SkinIndex).Name;
    }
    public override void SetSkinForChar(CharacterSkin characterSkin)
    {
        characterSkin.SetPant(m_Data.GetSkinInfo(m_SkinIndex).Material);
    }
}
