using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] PlayerController m_Player;
    [SerializeField] EnemyPooller m_Pooller;
    [SerializeField] Transform m_EnemyHolder;
    [SerializeField] Transform m_WeaponHolder;
    [SerializeField] int m_EnemyCount;
    [SerializeField] int m_EnemySpawnPerTime;
    [SerializeField] float m_MaxRadius;
    [SerializeField] float m_MinRadius;
    [SerializeField] NavMeshSurface m_MeshSurface;
    public bool IsCompleteInit;
    List<GameObject> m_EnemyOnMap = new List<GameObject>();
    int m_EnemyRemaining;
    void Start()
    {
        m_EnemyRemaining = m_EnemyCount;
        InitEnemy();
    }

    void InitEnemy()
    {
        for (int i = 0; i < m_EnemySpawnPerTime; i++)
        {
            SpawnEnemy();
        }
    }
    private void Update()
    {
        if (m_EnemyRemaining > 0 && m_EnemyOnMap.Count < m_EnemySpawnPerTime)
        {
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        float spawnRadius = Random.Range(m_MinRadius, m_MaxRadius);
        Vector2 randomDirection = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomDirection.x, 0, randomDirection.y);
        if (!CheckPlayer(spawnPosition))
        {
            GameObject enemy = m_Pooller.Spawn(m_EnemyHolder, spawnPosition, Quaternion.identity);
            int levelRand = m_Player.Level + Random.Range(0, 3);
            enemy.GetComponent<EnemyController>().ReBorn(this, m_MeshSurface, m_WeaponHolder,levelRand);
            m_EnemyRemaining--;
            m_EnemyOnMap.Add(enemy);
        }
        else
        {
            SpawnEnemy();
        }
    }
    bool CheckPlayer(Vector3 a_Pos)
    {
        Collider[] hits = Physics.OverlapSphere(a_Pos, 25, 1<<6);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].GetComponent<PlayerController>() != null)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void Despawn(EnemyController enemy)
    {
        m_Pooller.Despawn(enemy.gameObject);
        m_EnemyOnMap.Remove(enemy.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_MaxRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_MinRadius);
    }
}
