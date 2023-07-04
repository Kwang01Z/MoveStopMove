using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWeaponItem : UICanvas
{
    Dictionary<string, GameObject> m_WeaponHolder = new Dictionary<string, GameObject>();
    List<string> m_WeaponKey = new List<string>();
    int m_CurrentWeaponIndex = 0;
    public static UIWeaponItem Instance;
    public override void Init()
    {
        UIManager.Instance.PushCanvas<UIWeaponItem>(this);
        Instance = this;
        gameObject.SetActive(false);
        m_WeaponKey = WeaponManager.Instance.GetListWeapons();
        base.Init();
    }
    public override void Open()
    {
        base.Open();
        UIManager.Instance.TurnItemCamera(true);
        GetWeapon(m_CurrentWeaponIndex);
    }
    public override void Close()
    {
        base.Close();
        UIManager.Instance.TurnItemCamera(true);
    }
    public void ChangeWeapon(int a_int)
    {
        if (m_CurrentWeaponIndex + a_int > m_WeaponKey.Count - 1)
        {
            m_CurrentWeaponIndex = 0;
        }
        else if (m_CurrentWeaponIndex + a_int < 0)
        {
            m_CurrentWeaponIndex = m_WeaponKey.Count - 1;
        }
        else
        {
            m_CurrentWeaponIndex += a_int;
        }
        GetWeapon(m_CurrentWeaponIndex);
    }
    GameObject GetWeapon(int a_index)
    {
        string weaponKey = m_WeaponKey[a_index];
        if (!m_WeaponHolder.ContainsKey(weaponKey))
        {
            Weapon weapon = WeaponManager.Instance.GetWeapon(weaponKey, transform);
            weapon.gameObject.layer = 5;
            m_WeaponHolder.Add(weaponKey, weapon.gameObject);
        }
        DisplayWeapon(weaponKey);
        return m_WeaponHolder[weaponKey];
    }
    void DisplayWeapon(string a_name)
    {
        foreach (KeyValuePair<string, GameObject> pair in m_WeaponHolder)
        {
            if(pair.Value != null)
            pair.Value.gameObject.SetActive(pair.Key.Equals(a_name));
        }
    }
    public WeaponInfo GetCurrentWeaponInfo()
    {
        return WeaponManager.Instance.GetWeaponInfo(m_CurrentWeaponIndex);
    }
}
