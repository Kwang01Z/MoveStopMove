using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShieldDisplay : UICanvas
{
    Dictionary<string, GameObject> m_ShieldSkinHolder = new Dictionary<string, GameObject>();
    [SerializeField] List<string> m_ShieldKey = new List<string>();
    int m_CurrentItemIndex = 0;
    public static UIShieldDisplay Instance;
    public override void Init()
    {
        UIManager.Instance.PushCanvas<UIShieldDisplay>(this);
        gameObject.SetActive(false);
        m_ShieldKey = SkinManager.Instance.GetListShieldSkin();
        Instance = this;
        base.Init();
    }
    public override void Open()
    {
        base.Open();
        UIManager.Instance.TurnItemCamera(true);
        GetShieldSkin(0);
    }
    public override void Close()
    {
        base.Close();
        UIManager.Instance.TurnItemCamera(false);
    }
    public void ChangeSkin(int a_int)
    {
        if (m_CurrentItemIndex + a_int > m_ShieldKey.Count - 1)
        {
            m_CurrentItemIndex = 0;
        }
        else if (m_CurrentItemIndex + a_int < 0)
        {
            m_CurrentItemIndex = m_ShieldKey.Count - 1;
        }
        else
        {
            m_CurrentItemIndex += a_int;
        }
        GetShieldSkin(m_CurrentItemIndex);
    }
    GameObject GetShieldSkin(int a_index)
    {
        string shieldKey = m_ShieldKey[a_index];
        if (!m_ShieldSkinHolder.ContainsKey(shieldKey))
        {
            GameObject headSkin = SkinManager.Instance.GetShieldSkin(shieldKey, transform);
            headSkin.layer = 5;
            m_ShieldSkinHolder.Add(shieldKey, headSkin);
        }
        DisplaySkin(shieldKey);
        return m_ShieldSkinHolder[shieldKey];
    }
    void DisplaySkin(string a_name)
    {
        foreach (KeyValuePair<string, GameObject> pair in m_ShieldSkinHolder)
        {
            if (pair.Value != null)
                pair.Value.SetActive(pair.Key.Equals(a_name));
        }
    }
    public ShieldInfo GetCurrentSkin()
    {
        return SkinManager.Instance.GetShieldSkinInfo(m_ShieldKey[m_CurrentItemIndex]);
    }
}
