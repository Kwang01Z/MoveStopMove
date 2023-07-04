using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Z : Weapon
{
    [SerializeField] float m_RotationSpeed = 720f; // Độ xoay tối đa của vũ khí
    Vector3 m_BeginAttackPos;
    Vector3 m_MoveDirection;
    bool m_IsReturn;
    public override void StartAttack(Vector3 a_TargetPos, Transform a_WeaponPool, float a_MaxDistance, Character a_character)
    {
        base.StartAttack(a_TargetPos, a_WeaponPool, a_MaxDistance, a_character);
        m_MoveDirection = (m_TargetPos - transform.position).normalized;
        transform.rotation = Quaternion.Euler(90, 0, 0);
        m_BeginAttackPos = transform.position;
        m_IsReturn = false;
    }
    protected override void UpdateAttack()
    {
        base.UpdateAttack();
        MoveUpdate();
        RotateUpdate();
    }
    void MoveUpdate()
    {
        if (Vector3.Distance(transform.position, m_BeginAttackPos) >= m_MaxDistance && !m_IsReturn)
        {
            m_IsReturn = true;
        }
        if (m_IsReturn)
        {
            if ((Vector3.Distance(transform.position, m_BeginAttackPos) <= 0.2f))
            {
                EndAttack();
                return;
            }
            m_MoveDirection = (m_BeginAttackPos - transform.position).normalized;
        }

        transform.position += m_MoveDirection * m_SpeedMove * Time.deltaTime;
    }
    void RotateUpdate()
    {
        transform.Rotate(Vector3.forward, m_RotationSpeed * Time.deltaTime);
    }
}
