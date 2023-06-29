using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShopDisplay : MonoBehaviour
{
    [SerializeField] WeaponManager m_Data;
    [SerializeField] List<WeaponType> m_WeaponTypes;
    WeaponInfo m_CurrentWeapon;
    public WeaponInfo CurrentWeapon => m_CurrentWeapon;
    
    int m_CurrentWeaponIndex = 0;
    void Start()
    {
        m_WeaponTypes = m_Data.GetData().GetTypes();
        m_CurrentWeapon = m_Data.GetData().GetWeapon(m_WeaponTypes[m_CurrentWeaponIndex]);
        m_Data?.GetWeapon(m_CurrentWeapon.Type);
    }
    public void ChangeWeapon(int itemIndex)
    {
        if (m_CurrentWeaponIndex + itemIndex > m_WeaponTypes.Count - 1)
        {
            m_CurrentWeaponIndex = 0;
        }
        else if (m_CurrentWeaponIndex + itemIndex < 0)
        {
            m_CurrentWeaponIndex = m_WeaponTypes.Count - 1;
            
        }
        else
        {
            m_CurrentWeaponIndex += itemIndex;
        }
        m_CurrentWeapon = m_Data.GetData().GetWeapon(m_WeaponTypes[m_CurrentWeaponIndex]);
        m_Data?.GetWeapon(m_CurrentWeapon.Type);
        
    }
    
    public string GetNameCurrentWeapon()
    {
        return m_CurrentWeapon.NameDisplay;
    }
}
