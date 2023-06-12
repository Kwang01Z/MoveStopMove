using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    Vector3 m_BeginAttackPos;
    Vector3 m_MoveDirection;
    public override void StartAttack(Vector3 a_TargetPos, Transform a_WeaponPool, float a_MaxDistance)
    {
        base.StartAttack(a_TargetPos, a_WeaponPool, a_MaxDistance);
        m_MoveDirection = (m_TargetPos - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(-Vector3.up, -m_MoveDirection);
        m_BeginAttackPos = transform.position;
    }
    protected override void UpdateAttack()
    {
        base.UpdateAttack();
        MoveUpdate();
    }
    void MoveUpdate()
    {
        if (Vector3.Distance(transform.position, m_BeginAttackPos) >= m_MaxDistance)
        {
            // Đã đến vị trí mục tiêu, không di chuyển nữa
            EndAttack();
            return;
        }
        transform.position += m_MoveDirection * m_SpeedMove * Time.deltaTime;
    }
    protected override void EndAttack()
    {
        base.EndAttack();
    }
}
