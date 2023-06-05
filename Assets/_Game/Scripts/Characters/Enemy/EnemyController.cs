using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : CharacterControllerBase
{
    [SerializeField] NavMeshAgent m_Agent;
    [SerializeField] NavMeshSurface m_MeshSurface;
    [SerializeField] float m_MoveRadius = 20f;
    Vector3 m_NextPos;
    float m_TimeMove = 0;
    bool m_IsGettingNextPos;
    private void Reset()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }
    protected override void Start()
    {
        base.Start();
        m_NextPos = GetRandomNavMeshPosition();
    }
    protected override void Update()
    {
        base.Update();
        MoveToRandomPos();
    }
    void MoveToRandomPos()
    {
        m_TimeMove += Time.deltaTime;
        if ((Vector3.Distance(transform.position, m_NextPos) < 0.5f || m_TimeMove > 8) && !m_IsGettingNextPos)
        {
            m_IsGettingNextPos = true;
            Invoke(nameof(ResetNextPos), 3); 
        }
        m_MoveVelocity = m_Agent.velocity;
    }
    void ResetNextPos()
    {
        m_TimeMove = 0;
        m_NextPos = GetRandomNavMeshPosition();
        m_Agent.SetDestination(m_NextPos);
        m_IsGettingNextPos = false;
    }
    Vector3 GetRandomNavMeshPosition()
    {
        // Tạo một vector ngẫu nhiên trong không gian 3D
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 moveDirection = new Vector3(randomDirection.x, 0, randomDirection.y);
        moveDirection *= m_MoveRadius; // Điều chỉnh bán kính tìm kiếm
        // Lấy ngẫu nhiên một điểm trên NavMesh
        Vector3 randomPosition = Vector3.zero;
        if (NavMesh.SamplePosition(transform.position + moveDirection, out NavMeshHit hit, 5f, NavMesh.AllAreas))
        {
            randomPosition = hit.position;
        }
        return randomPosition;
    }
}
