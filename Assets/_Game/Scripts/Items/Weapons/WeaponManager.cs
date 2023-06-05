using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    Weapon m_Weapon;
    void Awake()
    {
        m_Weapon = GetComponentInChildren<Weapon>();
    }

    public Weapon GetWeapon()
    {
        return m_Weapon;
    }
}
