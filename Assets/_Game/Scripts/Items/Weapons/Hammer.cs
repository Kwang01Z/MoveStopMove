using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer :  Weapon
{
    //[SerializeField] float m_MaxHeight = 5f; // Độ cao tối đa của vũ khí
    [SerializeField] float m_RotationSpeed = 240f; // Độ xoay tối đa của vũ khí
    float m_ElapsedTime; // Thời gian đã trôi qua
    Vector3 m_MoveDirection;
    public override void StartAttack(Vector3 a_TargetPos, Transform a_WeaponPool)
    {
        base.StartAttack(a_TargetPos,a_WeaponPool);
        m_ElapsedTime = 0;
        m_MoveDirection = (m_TargetPos - transform.position).normalized;
        transform.rotation = Quaternion.Euler(90, 0, 0);
    }
    protected override void UpdateAttack()
    {
        base.UpdateAttack();
        MoveUpdate();
        RotateUpdate();
    }
    protected override void EndAttack()
    {
        base.EndAttack();
    }
    void MoveUpdate()
    {
        m_ElapsedTime += Time.deltaTime;

        if (Vector3.Distance(transform.position,m_TargetPos) < 0.5f || m_ElapsedTime > 10)
        {
            // Đã đến vị trí mục tiêu, không di chuyển nữa
            EndAttack();
            return;
        }
        transform.position += m_MoveDirection * m_SpeedMove * Time.deltaTime;
    }
    void RotateUpdate()
    {
        transform.Rotate(Vector3.forward, m_RotationSpeed * Time.deltaTime);
    }
}
