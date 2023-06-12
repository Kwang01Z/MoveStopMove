using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyPooller m_Pooller;
    [SerializeField] Transform m_EnemyHolder;
    [SerializeField] Transform m_WeaponHolder;
    [SerializeField] int m_EnemyCount;
    [SerializeField] float m_MaxRadius;
    [SerializeField] float m_MinRadius;
    [SerializeField] NavMeshSurface m_MeshSurface;
    public bool IsCompleteInit;
    void Start()
    {
        InitEnemy();
    }

    void InitEnemy()
    {
        for (int i = 0; i < m_EnemyCount; i++)
        {
            float spawnRadius = Random.Range(m_MinRadius, m_MaxRadius);
            Vector2 randomDirection = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 spawnPosition = new Vector3(randomDirection.x, 0, randomDirection.y);
            GameObject enemy = m_Pooller.Spawn(m_EnemyHolder, spawnPosition, Quaternion.identity);
            enemy.GetComponent<EnemyController>().SetSpawner(this);
            enemy.GetComponent<EnemyController>().SetMeshSurface(m_MeshSurface);
            enemy.GetComponent<EnemyController>().SetWeaponHolder(m_WeaponHolder);
        }
    }
    public void Despawn(EnemyController enemy)
    {
        m_Pooller.Despawn(enemy.gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_MaxRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_MinRadius);
    }
}
