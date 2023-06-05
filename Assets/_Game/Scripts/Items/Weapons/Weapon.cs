using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float m_SpeedMove = 10f;
    protected Vector3 m_TargetPos;
    protected Transform m_WeaponParent;
    protected Vector3 m_OldPosision;
    protected Quaternion m_OldRotation;
    protected bool m_IsAttacking;
    protected void Update()
    {
        if (!m_IsAttacking) return;
        UpdateAttack();
    }
    public virtual void StartAttack(Vector3 a_TargetPos, Transform a_WeaponPool)
    {
        if (m_IsAttacking) return;
        // Lưu vi trí cũ
        m_WeaponParent = transform.parent;
        m_OldPosision = transform.localPosition;
        m_OldRotation = transform.localRotation;
        // Thiết lập tt cơ bản
        m_TargetPos = new Vector3(a_TargetPos.x, transform.position.y, a_TargetPos.z);
        transform.parent = a_WeaponPool;
        m_IsAttacking = true;
    }
    protected virtual void UpdateAttack()
    { 
    
    }
    protected virtual void EndAttack()
    {
        m_IsAttacking = false;
        transform.parent = m_WeaponParent;
        transform.localPosition = m_OldPosision;
        transform.localRotation = m_OldRotation;
    }
    public bool IsAttacking()
    {
        return m_IsAttacking;
    }
}
