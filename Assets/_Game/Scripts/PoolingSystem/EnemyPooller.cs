using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPooller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m_Enemy;
    ObjectSpawner m_Spawner;
    void Awake()
    {
        Init();
    }
    void Init()
    {
        m_Spawner = new ObjectSpawner(transform, m_Enemy, 20);
    }
    public EnemyController Spawn(Transform a_parent, Vector3 a_pos, Quaternion a_quat)
    {
        GameObject obj = m_Spawner.Spawn(a_parent, a_pos, a_quat);
        return Cache.GetEnemy(obj);
    }
    public void Despawn(GameObject a_obj)
    {
        m_Spawner.Despawn(transform, a_obj);
    }
}
