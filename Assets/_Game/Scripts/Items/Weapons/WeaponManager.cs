using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class WeaponManager : MonoBehaviour
{
    [SerializeField] WeaponData m_WeaponData;
    [SerializeField] Transform m_WeaponHolder;
    List<Weapon> m_Weapons;
    private void Awake()
    {
        m_Weapons = GetComponentsInChildren<Weapon>().ToList();
        DisableWeapon();
    }
    private void Start()
    {
        
    }
    public WeaponType GetWeaponTypeRandom()
    {
        WeaponType result = WeaponType.HAMMER;
        List<WeaponType> weaponTypes = m_WeaponData.GetTypes();
        int rand = Random.Range(0, weaponTypes.Count);
        result = weaponTypes[rand];
        return result;
    }
    public GameObject GetWeapon(WeaponType a_Type, int a_Level)
    {
        DisableWeapon();
        GameObject result = null;
        WeaponInfo info = m_WeaponData.GetWeapon(a_Type, a_Level);
        Weapon weapon = GetWeaponFromHolder(info.Prefab);
        if (weapon == null)
        {
            result = Instantiate(info.Prefab.gameObject, this.transform);
            m_Weapons.Add(result.GetComponent<Weapon>());
        }
        else
        {
            weapon.gameObject.SetActive(true);
            result = weapon.gameObject;
        }
        result.GetComponent<Weapon>().SetWeaponParent(transform);
        return result;
    }
    void DisableWeapon()
    {
        if (m_Weapons.Count > 0)
        {
            m_Weapons.ForEach((weapon) =>
            {
                weapon.gameObject.SetActive(false);
            });
        }
    }
    Weapon GetWeaponFromHolder(Weapon a_Weapon)
    {
        Weapon result = null;
        m_Weapons.ForEach((weapon) =>
        {
            if (weapon.GetType().Equals(a_Weapon.GetType()))
            {
                result = weapon;
            }
        });
        return result;
    }
    public Transform GetHolder()
    {
        return m_WeaponHolder;
    }
    public void SetHolder(Transform a_Holder)
    {
        m_WeaponHolder = a_Holder;
    }
}
