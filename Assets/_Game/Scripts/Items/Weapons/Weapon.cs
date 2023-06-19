using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float m_SpeedMove = 10f;
    [SerializeField] protected Collider m_Collider;
    protected CharacterControllerBase m_CharacterUse;
    protected Vector3 m_TargetPos;
    protected Transform m_WeaponParent;
    protected Vector3 m_OldPosision;
    protected Quaternion m_OldRotation;
    protected bool m_IsAttacking;
    protected float m_MaxDistance;
    private void Awake()
    {
        m_Collider.enabled = false;
    }
    protected void Update()
    {
        if (!m_IsAttacking) return;
        UpdateAttack();
    }
    public virtual void StartAttack(Vector3 a_TargetPos, Transform a_WeaponPool, float a_MaxDistance, CharacterControllerBase a_character)
    {
        if (m_IsAttacking) return;
        // Lưu vi trí cũ
        m_OldPosision = transform.localPosition;
        m_OldRotation = transform.localRotation;
        // Thiết lập tt cơ bản
        m_TargetPos = new Vector3(a_TargetPos.x, transform.position.y, a_TargetPos.z);
        transform.parent = a_WeaponPool;
        m_IsAttacking = true;

        m_MaxDistance = a_MaxDistance;
        m_Collider.enabled = true;
        m_CharacterUse = a_character;
    }
    protected virtual void UpdateAttack()
    { 
    
    }
    protected virtual void EndAttack()
    {
        m_IsAttacking = false;
        if (m_WeaponParent)
        {
            transform.parent = m_WeaponParent;
        }
        else
        {
            Destroy(gameObject);
        }
        transform.localPosition = m_OldPosision;
        transform.localRotation = m_OldRotation;
        m_Collider.enabled = false;
    }
    public bool IsAttacking()
    {
        return m_IsAttacking;
    }
    public void SetWeaponParent(Transform a_parent)
    {
        m_WeaponParent = a_parent;
    }
    private void OnTriggerEnter(Collider other)
    {
        CharacterControllerBase character = other.GetComponent<CharacterControllerBase>();
        if (character != null && character.GetWeapon() != this)
        {
            character.Damaged(m_CharacterUse);
            EndAttack();
        }
    }
}
