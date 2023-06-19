using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField] List<WeaponInfo> m_Weapons;
    public WeaponInfo GetWeapon(WeaponType a_type)
    {
        WeaponInfo result = null;
        m_Weapons.ForEach((weapon) =>
        {
            if (weapon.Type.Equals(a_type))
            {
                result = weapon;
            }
        });
        return result;
    }
    public List<WeaponType> GetTypes()
    {
        List<WeaponType> result = new List<WeaponType>();
        m_Weapons.ForEach((weapon) =>
        {
            result.Add(weapon.Type);
        });
        return result;
    }
    public List<WeaponInfo> GetListWeapon()
    {
        return m_Weapons;
    }
}
[System.Serializable]
public class WeaponInfo
{
    public WeaponType Type;
    public Weapon Prefab;
    public int Price;
    public string NameDisplay;
}
public enum WeaponType
{
    HAMMER = 0,
    LOLLIPOP = 1,
    KNIFE = 2,
    CANDY_CANE = 3,
    BOOMERANG = 4,
    SWERPLY_POP = 5,
    AXE = 6,
    ICE_CREAM_CONE = 7,
    BATTLE_AXE = 8,
    Z = 9,
    ARROW = 10,
    UZI = 11
}
