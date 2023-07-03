using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BotPooler : Singleton<BotPooler>
{
    [SerializeField] Bot m_BotPF;
    [SerializeField] int m_PreInitAmount;
    ObjectSpawner m_Spawner;
    public void Init()
    {
        m_Spawner = new ObjectSpawner(transform, m_BotPF.gameObject, m_PreInitAmount);
    }
    public Bot Spawn(Vector3 a_pos, Quaternion a_quat)
    {
        GameObject obj = m_Spawner.Spawn(transform, a_pos, a_quat);
        return Cache.GetBot(obj);
    }
    public void Despawn(Bot a_obj)
    {
        m_Spawner.Despawn(transform, a_obj.gameObject);
    }
}
