using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObject/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    [SerializeField] List<WeaponInfo> m_Weapons;
    public WeaponInfo GetWeapon(string a_name)
    {
        WeaponInfo result = null;
        m_Weapons.ForEach((weapon) =>
        {
            if (a_name.Equals(weapon.SaveTxt))
            {
                result = weapon;
            }
        });
        return result;
    }
    public List<WeaponInfo> GetWeaponInfos()
    {
        return m_Weapons;
    }
    public List<string> GetListWeapon()
    {
        List<string> result = new List<string>();
        m_Weapons.ForEach((weapon) =>
        {
            result.Add(weapon.SaveTxt);
        });
        return result;
    }
    public string GetRandomWeapon()
    {
        int rand = Random.Range(0, m_Weapons.Count);
        return m_Weapons[rand].SaveTxt;
    }
}
[System.Serializable]
public class WeaponInfo
{
    public string Name;
    public WeaponType Type;
    public Weapon Prefab;
    public int Price;
    public string SaveTxt => Type.ToString() + "_" + Name;
}
