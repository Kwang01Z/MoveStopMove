using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache
{
    static Dictionary<Collider, CharacterControllerBase> m_Characters = new Dictionary<Collider, CharacterControllerBase>();
    public static CharacterControllerBase GetCharacter(Collider collider)
    {
        if (!m_Characters.ContainsKey(collider))
        {
            m_Characters.Add(collider, collider.GetComponent<CharacterControllerBase>());
        }
        return m_Characters[collider];
    }
    static Dictionary<GameObject, EnemyController> m_Enenemies = new Dictionary<GameObject, EnemyController>();
    public static EnemyController GetEnemy(GameObject gameObject)
    {
        if (!m_Enenemies.ContainsKey(gameObject))
        {
            m_Enenemies.Add(gameObject, gameObject.GetComponent<EnemyController>());
        }
        return m_Enenemies[gameObject];
    }
}
