using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPantSkinItem : UICanvas
{
    [SerializeField] List<string> m_PantKey = new List<string>();
    [SerializeField] Image m_ImageDisplay;
    int m_CurrentItemIndex = 0;
    public static UIPantSkinItem Instance;
    public override void Init()
    {
        UIManager.Instance.PushCanvas<UIPantSkinItem>(this);
        gameObject.SetActive(false);
        m_PantKey = SkinManager.Instance.GetListPantSkin();
        Instance = this;
        base.Init();
    }
    public override void Open()
    {
        base.Open();
        UIManager.Instance.TurnItemCamera(true);
        GetPantSkin(0);
    }
    public override void Close()
    {
        base.Close();
        UIManager.Instance.TurnItemCamera(false);
    }
    public void ChangeSkin(int a_int)
    {
        if (m_CurrentItemIndex + a_int > m_PantKey.Count - 1)
        {
            m_CurrentItemIndex = 0;
        }
        else if (m_CurrentItemIndex + a_int < 0)
        {
            m_CurrentItemIndex = m_PantKey.Count - 1;
        }
        else
        {
            m_CurrentItemIndex += a_int;
        }
        GetPantSkin(m_CurrentItemIndex);
    }
    void GetPantSkin(int a_index)
    {
        string pantKey = m_PantKey[a_index];
        m_ImageDisplay.sprite = SkinManager.Instance.GetPantSkin(pantKey).IconDisplay;
    }
    public PantSkinInfo GetCurrentSkin()
    {
        return SkinManager.Instance.GetPantSkin(m_PantKey[m_CurrentItemIndex]);
    }
}
