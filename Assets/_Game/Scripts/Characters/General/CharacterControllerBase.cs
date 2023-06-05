using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    [SerializeField] protected CharacterAnimator m_CharacterAnimator;
    [SerializeField] protected float m_AttackRange;
    [SerializeField] protected WeaponManager m_WeaponManager;
    protected Weapon m_MainWeapon;
    protected Vector3 m_MoveVelocity = Vector3.zero;
    private void Reset()
    {
        m_CharacterAnimator = GetComponentInChildren<CharacterAnimator>();
    }
    protected virtual void Start()
    {
        m_MainWeapon = m_WeaponManager?.GetWeapon();
    }
    protected virtual void Update()
    {
        
    }
    protected virtual void LateUpdate()
    {
        UpdateAnim();
    }
    protected void UpdateAnim()
    {
        if (m_MoveVelocity.magnitude > 0.3f)
        {
            m_CharacterAnimator.ChangeState(CharacterState.Run);
        }
        else if (m_MainWeapon != null && !m_MainWeapon.IsAttacking() 
            && LoadTaget() != null && m_WeaponManager != null)
        {
            m_CharacterAnimator.ChangeState(CharacterState.Attack);
        }
        else
        {
            m_CharacterAnimator.ChangeState(CharacterState.Idle);
        }
    }
    public void OnAttack()
    {
        CharacterControllerBase target = LoadTaget();
        if (target != null && !m_MainWeapon.IsAttacking())
        {
            ThrowWeapon(target.transform.position);
            Vector3 dir = target.transform.position - transform.position;
            RotateObject(dir);
        }
    }
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
        m_MainWeapon.StartAttack(a_TargetPos, transform.parent);
    }
    protected void RotateObject(Vector3 rot)
    {
        if (rot.magnitude == 0) return;
        Quaternion rotation = Quaternion.LookRotation(rot, Vector3.up);
        m_CharacterAnimator.transform.rotation = rotation;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_AttackRange);
    }
}
