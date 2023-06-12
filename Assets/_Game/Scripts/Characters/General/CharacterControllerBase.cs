using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    [SerializeField] protected CharacterAnimator m_CharacterAnimator;
    [SerializeField] protected float m_AttackRange;
    [SerializeField] protected WeaponManager m_WeaponManager;
    [SerializeField] protected WeaponType m_WeaponTypeCurrent = WeaponType.KNIFE;
    [SerializeField] protected int m_WeaponLevel = 0;
    protected bool m_IsDead;
    public bool IsDead => m_IsDead;
    protected Weapon m_MainWeapon;
    protected Vector3 m_MoveVelocity = Vector3.zero;
    private void Reset()
    {
        m_CharacterAnimator = GetComponentInChildren<CharacterAnimator>();
    }
    protected virtual void Start()
    {
        
    }
    protected virtual void Update()
    {
    }
    protected virtual void LateUpdate()
    {
        if (m_IsDead) return;
        UpdateAnim();
    }
    protected virtual void UpdateAnim()
    { }
    public void OnAttack()
    {
        CharacterControllerBase target = LoadTaget();
        if (target != null && !m_MainWeapon.IsAttacking())
        {
            Vector3 dir = target.transform.position - transform.position;
            RotateObject(dir);
            ThrowWeapon(target.transform.position); 
        }
    }
    public virtual void EndAttack()
    { }
    protected CharacterControllerBase LoadTaget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_AttackRange);
        if (colliders.Length <= 0) return null;
        // Get list character
        List<CharacterControllerBase> characters = new List<CharacterControllerBase>();
        foreach (Collider collider in colliders)
        {
            CharacterControllerBase character = collider.GetComponent<CharacterControllerBase>();
            if (character != null && character != this)
            {
                characters.Add(character);
            }
        }
        // Get Character with distance min
        if (characters.Count <= 0) return null;
        CharacterControllerBase result = characters[0];
        if (characters.Count > 1)
        {
            for (int i = 1; i < characters.Count - 1; i++)
            {
                if (Vector3.Distance(transform.position, characters[i].transform.position) <
                    Vector3.Distance(transform.position, result.transform.position))
                {
                    result = characters[i];
                }
            }
        }
        return result;
    }
    protected void ThrowWeapon(Vector3 a_TargetPos)
    {
        m_MainWeapon.StartAttack(a_TargetPos, m_WeaponManager.GetHolder(), m_AttackRange);
    }
    protected virtual void RotateObject(Vector3 rot)
    {
        if (rot.magnitude == 0) return;
        Quaternion rotation = Quaternion.LookRotation(rot, Vector3.up);
        m_CharacterAnimator.transform.rotation = rotation;
    }
    public float GetAttackRange()
    {
        return m_AttackRange;
    }
    public virtual void Damaged()
    {
        m_IsDead = true;
        m_CharacterAnimator.ChangeState(CharacterState.Dead);
        OnDead();
    }
    protected virtual void OnDead()
    { 
        
    }
    public Weapon GetWeapon()
    {
        return m_MainWeapon;
    }
    public void SetWeaponHolder(Transform a_weaponHolder)
    {
        m_WeaponManager.SetHolder(a_weaponHolder);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_AttackRange);
    }
}
