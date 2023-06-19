using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlayer : MonoBehaviour
{
    public static DataPlayer Instance;
    int m_Level;
    public int Level => m_Level;
    public int m_Coin;
    public int Coin => m_Coin;
    [SerializeField] PlayerController m_Player;
    [SerializeField] List<WeaponType> m_OwnedWeapons;
    WeaponType m_EquipedWeapon = WeaponType.HAMMER;
    public WeaponType EquipedWeapon => m_EquipedWeapon;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        LoadData();
    }

    public bool IsOwnedWeapon(WeaponType a_Type)
    {
        bool result = false;
        if (m_OwnedWeapons.Count > 0)
        {
            m_OwnedWeapons.ForEach((type) =>
            {
                if (type.Equals(a_Type)) result = true;   
            });
        }
        return result;
    }
    public void LoadData()
    {
        if (m_OwnedWeapons.Count <= 0)
        {
            m_OwnedWeapons.Add(WeaponType.HAMMER);
        }
    }
    public void AddWeapon(WeaponType a_weapon)
    {
        if (!IsOwnedWeapon(a_weapon))
        {
            m_OwnedWeapons.Add(a_weapon);
        }
    }
    public void AddCoin(int a_coin)
    {
        m_Coin += a_coin;
    }
    public void ChangeWeapon(WeaponType a_WeaponType)
    {
        m_EquipedWeapon = a_WeaponType;
        m_Player.LoadWeapon();
    }
}
