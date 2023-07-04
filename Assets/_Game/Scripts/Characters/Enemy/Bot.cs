using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Bot : Character
{
    [SerializeField] NavMeshAgent m_Agent;
    [SerializeField] float m_MoveRadius = 20f;
    [SerializeField] float m_TimeIdle = 3;
    [SerializeField] ParticleSystem m_RebornParticle;
    [SerializeField] AudioSource m_AudioSource;
    AudioClip deathSound;
    IState<Bot> m_State;
    public float TimeIdle => m_TimeIdle;
    public NavMeshAgent Agent => m_Agent;
    private void Reset()
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        deathSound = Resources.Load<AudioClip>("Sounds/Die");
    }
    protected override void UpdateAnim()
    {
        base.UpdateAnim();
        if (GameManager.Instance.IsState(GameState.Gameplay))
        {
            m_State.OnExcute(this);
        }
        else
        {
            Stop();
        }
    }
    public void ChageState(IState<Bot> a_state)
    {
        if (!m_State.GetType().Equals(a_state.GetType()))
        {
            m_State.OnExit(this);
            m_State = a_state;
            m_State.OnEnter(this);
        }
    }
    public Vector3 GetRandomNavMeshPosition()
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
        else
        {
            return GetRandomNavMeshPosition();
        }
        return randomPosition;
    }
    protected override void OnDead()
    {
        base.OnDead();
        m_Agent.isStopped = true;
        //m_AudioSource.PlayOneShot(deathSound);
        StartCoroutine(DespawnEnemy());
    }
    IEnumerator DespawnEnemy()
    {
        yield return new WaitForSeconds(2);
        BotPooler.Instance.Despawn(this);
        LevelManager.Instance.ReleaseBot(this);
    }
    public override void EndAttack()
    {
        base.EndAttack();
        ChageState(new BotIdle());
    }
    public void ReBorn(int a_level)
    {
        SetDead(false);
        m_Level = a_level;
        m_State = new BotIdle();
        m_State.OnEnter(this);
        m_RebornParticle.Play();
        m_CurrentWeapon = WeaponManager.Instance.GetRandomWeapon();
        if (m_MainWeapon != null)
        {
            Destroy(m_MainWeapon.gameObject);
        }
        m_MainWeapon = WeaponManager.Instance.GetWeapon(m_CurrentWeapon, m_CharWeaponHolder);
        string headSkinRandom = SkinManager.Instance.GetRandomHeadSkinTxt();
        m_CharacterSkin.SetHeadSkin(headSkinRandom);
        string pantSkinRandom = SkinManager.Instance.GetRandomPantSkinTxt();
        m_CharacterSkin.SetPant(SkinManager.Instance.GetPantSkin(pantSkinRandom).Material);
    }
    void Stop()
    {
        m_Agent.isStopped = true;
        ChageState(new BotIdle());
    }
}
