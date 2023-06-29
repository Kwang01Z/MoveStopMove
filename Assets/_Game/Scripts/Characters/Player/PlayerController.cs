using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterControllerBase
{
    [SerializeField] UltimateJoystick m_Joystick;
    [SerializeField] Rigidbody m_Rigidbody;
    [SerializeField] float m_MoveSpeed;
    [SerializeField] IndicatorEnemy m_Indicator;
    [SerializeField] EndGameManager m_EndGameManager;
    Vector3 m_MoveDirection = Vector3.zero;
    bool m_HadDeath;
    public bool HadDeath=>m_HadDeath;
    protected override void Start()
    {
        base.Start();
        LoadWeapon();
    }
    public void LoadWeapon()
    {
        m_WeaponTypeCurrent = DataPlayer.Instance.EquipedWeapon;
        m_MainWeapon = m_WeaponManager?.GetWeapon(m_WeaponTypeCurrent).GetComponent<Weapon>();
    }
    protected override void UpdateAnim()
    {
        MoveUpdate();
        IndicateEnemy();
        CharacterControllerBase target = LoadTaget();
        if (m_MoveVelocity.magnitude > 0.3f)
        {
            m_CharacterAnimator.ChangeState(CharacterState.Run);
        }
        else
        {
            if (target != null && !target.IsDead)
            {
                if (!m_MainWeapon.IsAttacking())
                {
                    m_CharacterAnimator.ChangeState(CharacterState.Attack);
                }
            }
            else
            {
                m_CharacterAnimator.ChangeState(CharacterState.Idle);
            }
        }
    }
    void MoveUpdate()
    {
        m_MoveDirection = new Vector3(m_Joystick.GetHorizontalAxis(), m_MoveDirection.y, m_Joystick.GetVerticalAxis()).normalized;
        m_MoveVelocity = m_MoveDirection * m_MoveSpeed;
        m_Rigidbody.velocity = m_MoveVelocity;
        m_Indicator.SetFollow(m_AttackRange, true);
        RotateObject(m_MoveDirection);
    }
    void IndicateEnemy()
    {
        CharacterControllerBase target = LoadTaget();
        if (target != null)
        {
            m_Indicator.SetTarget(target.transform.position);
        }
        else
        {
            m_Indicator.OffIndicator();
        }
    }
    public override void EndAttack()
    {
        base.EndAttack();
        m_CharacterAnimator.ChangeState(CharacterState.Idle);
    }
    protected override void OnDead()
    {
        base.OnDead();
        m_Rigidbody.velocity = Vector3.zero;
        m_Indicator.OffIndicator();
        StartCoroutine(OpenEndGameScene());
    }
    public IEnumerator OpenEndGameScene()
    {
        yield return new WaitForSeconds(2);
        DataPlayer.Instance.AddCoin(m_CoinClaim);
        m_EndGameManager.SetCoinClamped(m_CoinClaim);
        m_EndGameManager.SetRank();
        m_EndGameManager.SetEvaluteText();
        m_EndGameManager.SetNotification();
        GameController.Instance.ChangeState(new EndGameState());
    }
    public void Reborn()
    {
        m_CharacterAnimator.ChangeState(CharacterState.Idle);
        SetDead(false);
    }
    public void SetHadDeath(bool a_bool)
    {
        m_HadDeath = a_bool;
    }
}
