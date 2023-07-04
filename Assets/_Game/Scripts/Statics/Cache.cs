using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache
{
    static Dictionary<Collider, Character> m_Characters = new Dictionary<Collider, Character>();
    public static Character GetCharacter(Collider collider)
    {
        if (!m_Characters.ContainsKey(collider))
        {
            m_Characters.Add(collider, collider.GetComponent<Character>());
        }
        return m_Characters[collider];
    }
    static Dictionary<GameObject, Bot> m_Bots = new Dictionary<GameObject, Bot>();
    public static Bot GetBot(GameObject obj)
    {
        if (!m_Bots.ContainsKey(obj))
        {
            m_Bots.Add(obj, obj.GetComponent<Bot>());
        }
        return m_Bots[obj];
    }
    static Dictionary<GameObject, Weapon> m_Weapons = new Dictionary<GameObject, Weapon>();
}
