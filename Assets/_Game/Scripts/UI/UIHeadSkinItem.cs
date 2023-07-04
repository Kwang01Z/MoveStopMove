using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHeadSkinItem : UICanvas
{
    Dictionary<string, GameObject> m_HeadSkinHolder = new Dictionary<string, GameObject>();
    [SerializeField] List<string> m_HeadKey = new List<string>();
    int m_CurrentItemIndex = 0;
    public static UIHeadSkinItem Instance;
    public override void Init()
    {
        UIManager.Instance.PushCanvas<UIHeadSkinItem>(this);
        gameObject.SetActive(false);
        m_HeadKey = SkinManager.Instance.GetListHeadSkin();
        Instance = this;
        base.Init();
    }
    public override void Open()
    {
        base.Open();
        UIManager.Instance.TurnItemCamera(true);
        GetHeadSkin(0);
    }
    public override void Close()
    {
        base.Close();
        UIManager.Instance.TurnItemCamera(false);
    }
    public void ChangeSkin(int a_int)
    {
        if (m_CurrentItemIndex + a_int > m_HeadKey.Count - 1)
        {
            m_CurrentItemIndex = 0;
        }
        else if (m_CurrentItemIndex + a_int < 0)
        {
            m_CurrentItemIndex = m_HeadKey.Count - 1;
        }
        else
        {
            m_CurrentItemIndex += a_int;
        }
        GetHeadSkin(m_CurrentItemIndex);
    }
    GameObject GetHeadSkin(int a_index)
    {
        string headKey = m_HeadKey[a_index];
        if (!m_HeadSkinHolder.ContainsKey(headKey))
        {
            GameObject headSkin = SkinManager.Instance.GetHeadSkin(headKey,transform);
            headSkin.layer = 5;
            m_HeadSkinHolder.Add(headKey, headSkin);
        }
        DisplaySkin(headKey);
        return m_HeadSkinHolder[headKey];
    }
    void DisplaySkin(string a_name)
    {
        foreach (KeyValuePair<string, GameObject> pair in m_HeadSkinHolder)
        {
            if (pair.Value != null)
                pair.Value.SetActive(pair.Key.Equals(a_name));
        }
    }
    public HeadSkinInfo GetCurrentSkin()
    {
        return SkinManager.Instance.GetHeadSkinInfo(m_HeadKey[m_CurrentItemIndex]);
    }
}
