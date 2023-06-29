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
    List<EnemyController> m_EnemyOnMap = new List<EnemyController>();
    public int m_EnemyRemaining;
    public int EnemyRemaining => m_EnemyRemaining + m_EnemyOnMap.Count;
    public int EnemyCount => m_EnemyCount;
    bool m_IsEndGame;
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
        if (EnemyRemaining <= 0 && !m_IsEndGame)
        {
            m_IsEndGame = true;
            StartCoroutine(m_Player.OpenEndGameScene());
        }
    }
    void SpawnEnemy()
    {
        float spawnRadius = Random.Range(m_MinRadius, m_MaxRadius);
        Vector2 randomDirection = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomDirection.x, 0, randomDirection.y);
        if (!CheckCharacter(spawnPosition))
        {
            EnemyController enemy = m_Pooller.Spawn(m_EnemyHolder, spawnPosition, Quaternion.identity);
            int levelRand = m_Player.Level + Random.Range(0, 3);
            enemy.ReBorn(this, m_MeshSurface, m_WeaponHolder,levelRand);
            m_EnemyRemaining--;
            m_EnemyOnMap.Add(enemy);
        }
        else
        {
            SpawnEnemy();
        }
    }
    bool CheckCharacter(Vector3 a_Pos)
    {
        Collider[] hits = Physics.OverlapSphere(a_Pos, 25);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                if (Cache.GetCharacter(hits[i]) != null)
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
        m_EnemyOnMap.Remove(enemy);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_MaxRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_MinRadius);
    }
}
