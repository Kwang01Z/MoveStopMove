using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : CharacterControllerBase
{
    [SerializeField] NavMeshAgent m_Agent;
    [SerializeField] NavMeshSurface m_MeshSurface;
    [SerializeField] float m_MoveRadius = 20f;
    [SerializeField] float m_TimeIdle = 3;
    EnemySpawner m_Spawner;
    Vector3 m_NextPos;
    float m_TimeMove = 4;
    bool m_IsGettingNextPos;
    bool m_IsAttacking;
    private void Reset()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }
    protected override void Start()
    {
        base.Start();
        m_NextPos = GetRandomNavMeshPosition();
        m_WeaponTypeCurrent = m_WeaponManager.GetWeaponTypeRandom();
        m_MainWeapon = m_WeaponManager?.GetWeapon(m_WeaponTypeCurrent).GetComponent<Weapon>();
    }
    protected override void UpdateAnim()
    {
        base.UpdateAnim();
        CharacterControllerBase target = LoadTaget();
        if (target != null && !target.IsDead || GameController.Instance.IsMainMenuOn)
        {
            m_Agent.isStopped = true;
            if (!m_MainWeapon.IsAttacking() && !m_IsAttacking && !GameController.Instance.IsMainMenuOn)
            {
                m_CharacterAnimator.ChangeState(CharacterState.Attack);
            }
            else
            {
                m_CharacterAnimator.ChangeState(CharacterState.Idle);
            }
        }
        else
        {
            MoveToRandomPos();
            if (m_MoveVelocity.magnitude > 0.3f)
            {
                m_CharacterAnimator.ChangeState(CharacterState.Run);
                Vector3 dir = m_NextPos - transform.position;
                m_CharacterAnimator.transform.rotation = Quaternion.LookRotation(dir, transform.up);
            }
            else
            {
                m_CharacterAnimator.ChangeState(CharacterState.Idle);
            }
        }
    }
    void MoveToRandomPos()
    {
        m_TimeMove += Time.deltaTime;
        if ((Vector3.Distance(transform.position, m_NextPos) < 0.1f || m_TimeMove > 5) && !m_IsGettingNextPos)
        {
            m_IsGettingNextPos = true;
            StartCoroutine(ResetNextPos());
        }
        m_MoveVelocity = m_Agent.velocity;
    }

    IEnumerator ResetNextPos()
    {
        yield return new WaitForSeconds(m_TimeIdle);
        m_TimeMove = 0;
        m_NextPos = GetRandomNavMeshPosition();
        m_Agent.isStopped = false;
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
    protected override void OnDead()
    {
        base.OnDead();
        m_Agent.isStopped = true;
        StartCoroutine(DespawnEnemy());
    }
    IEnumerator DespawnEnemy()
    {
        yield return new WaitForSeconds(2);
        m_Spawner?.Despawn(this);
    }
    public override void EndAttack()
    {
        base.EndAttack();
        m_CharacterAnimator.ChangeState(CharacterState.Idle);
    }
    public void SetSpawner(EnemySpawner spawner)
    {
        m_Spawner = spawner;
    }
    public void SetMeshSurface(NavMeshSurface surface)
    {
        m_MeshSurface = surface;
    }
    public void ReBorn(EnemySpawner a_spawner, NavMeshSurface a_meshSurface, Transform a_weaponHolder, int a_level)
    {
        SetSpawner(a_spawner);
        SetMeshSurface(a_meshSurface);
        SetWeaponHolder(a_weaponHolder);
        SetDead(false);
        m_Level = a_level;
        m_CoinClaim = 0;
    }
}
