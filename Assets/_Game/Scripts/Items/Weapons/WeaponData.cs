using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField] List<WeaponInfo> m_Weapons;
    public WeaponInfo GetWeapon(WeaponType a_type, int level)
    {
        WeaponInfo result = null;
        m_Weapons.ForEach((weapon) =>
        {
            if (weapon.Type.Equals(a_type) && weapon.Level == level)
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
}
[System.Serializable]
public class WeaponInfo
{
    public WeaponType Type;
    public Weapon Prefab;
    public int Level;
}
public enum WeaponType
{
    HAMMER = 0,
    CANDY = 1,
    ARROW = 2,
    KNIFE = 3,
    BOOMERANG = 4,
    UZI = 5,
    Z = 6
}
