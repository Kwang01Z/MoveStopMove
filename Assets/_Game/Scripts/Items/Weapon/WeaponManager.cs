using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    public WeaponData _WeaponData;
    public Transform _WeaponHolder;
    public Weapon GetWeapon(string a_weapon, Transform a_parent)
    {
        GameObject obj = Instantiate(_WeaponData.GetWeapon(a_weapon).Prefab.gameObject, a_parent);
        return obj.GetComponent<Weapon>();
    }
    public string GetRandomWeapon()
    {
        return _WeaponData.GetRandomWeapon();
    }
    public List<string> GetListWeapons()
    {
        return _WeaponData.GetListWeapon();
    }
    public string GetWeaponTxtByIndex(int a_index)
    {
        return _WeaponData.GetListWeapon()[a_index];
    }
    public WeaponInfo GetWeaponInfo(int a_index)
    {
        return _WeaponData.GetWeaponInfos()[a_index];
    }
}