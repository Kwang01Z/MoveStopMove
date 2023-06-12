using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterControllerBase
{
    [SerializeField] UltimateJoystick m_Joystick;
    [SerializeField] Rigidbody m_Rigidbody;
    [SerializeField] float m_MoveSpeed;
    [SerializeField] IndicatorEnemy m_Indicator;
    
    Vector3 m_MoveDirection = Vector3.zero;
    
    protected override void Start()
    {
        base.Start();
        m_MainWeapon = m_WeaponManager?.GetWeapon(m_WeaponTypeCurrent, m_WeaponLevel).GetComponent<Weapon>();
    }
    protected override void Update()
    {
        base.Update();
        if (m_IsDead) return; 
        MoveUpdate();
        IndicateEnemy();
    }
    protected override void UpdateAnim()
    {
        base.UpdateAnim();
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
    }
}
