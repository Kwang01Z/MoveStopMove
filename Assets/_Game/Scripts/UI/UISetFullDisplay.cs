using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetFullDisplay : UICanvas
{
    [SerializeField] List<string> m_SetFullKey = new List<string>();
    [SerializeField] Image m_ImageDisplay;
    int m_CurrentItemIndex = 0;
    public static UISetFullDisplay Instance;
    public override void Init()
    {
        UIManager.Instance.PushCanvas<UISetFullDisplay>(this);
        gameObject.SetActive(false);
        m_SetFullKey = SkinManager.Instance.GetListSetFull();
        Instance = this;
        base.Init();
    }
    public override void Open()
    {
        base.Open();
        UIManager.Instance.TurnItemCamera(true);
        GetSetFull(0);
    }
    public override void Close()
    {
        base.Close();
        UIManager.Instance.TurnItemCamera(false);
    }
    public void ChangeSkin(int a_int)
    {
        if (m_CurrentItemIndex + a_int > m_SetFullKey.Count - 1)
        {
            m_CurrentItemIndex = 0;
        }
        else if (m_CurrentItemIndex + a_int < 0)
        {
            m_CurrentItemIndex = m_SetFullKey.Count - 1;
        }
        else
        {
            m_CurrentItemIndex += a_int;
        }
        GetSetFull(m_CurrentItemIndex);
    }
    void GetSetFull(int a_index)
    {
        string setKey = m_SetFullKey[a_index];
        m_ImageDisplay.sprite = SkinManager.Instance.GetSetFull(setKey).IconDisplay;
    }
    public SetFullInfo GetCurrentSkin()
    {
        return SkinManager.Instance.GetSetFull(m_SetFullKey[m_CurrentItemIndex]);
    }
}
