using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Transform m_CharWeaponHolder;
    [SerializeField] protected Weapon m_MainWeapon;
    [SerializeField] protected float m_AttackRange;
    
    protected CharacterAnimator m_Animator;
    protected CharacterSkin m_CharacterSkin;
    protected Vector3 m_MoveVelocity = Vector3.zero;
    protected bool m_IsDead;
    protected int m_Level;
    protected int m_CoinClaim;
    protected bool m_IsJustDead;
    protected string m_CurrentWeapon;
    public int CoinClaim => m_CoinClaim;
    public bool IsDead => m_IsDead;
    public int Level => m_Level;
    public float AttackRange => m_AttackRange;
    public CharacterAnimator Animator => m_Animator;
    private void Reset()
    {
        m_Animator = GetComponentInChildren<CharacterAnimator>();
    }
    protected virtual void Update()
    {
        if (m_IsDead)
        {
            m_Animator.ChangeState(CharacterState.Dead);
        }
        else
        {
            UpdateAnim();
        }
    }
    protected virtual void UpdateAnim()
    {
    }
    public Character LoadTaget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_AttackRange);
        if (colliders.Length <= 0) return null;
        // Get list character
        List<Character> characters = new List<Character>();
        foreach (Collider collider in colliders)
        {
            Character character = Cache.GetCharacter(collider);
            if (character != null && character != this)
            {
                characters.Add(character);
            }
        }
        // Get Character with distance min
        if (characters.Count <= 0) return null;
        Character result = characters[0];
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

    public virtual void EndAttack()
    {
        m_Animator.ChangeState(CharacterState.Idle);
    }

    internal void OnAttack()
    {
        Character target = LoadTaget();
        if(target != null)
        ThrowWeapon(target.transform.position);
    }
    protected void ThrowWeapon(Vector3 a_TargetPos)
    {
        m_MainWeapon.StartAttack(a_TargetPos, WeaponManager.Instance._WeaponHolder, m_AttackRange, this);
    }
    public virtual void RotateObject(Vector3 a_rot)
    {
        Quaternion rotation = Quaternion.LookRotation(a_rot, Vector3.up);
        m_Animator.transform.localRotation = rotation;
    }
    public Weapon GetWeapon()
    {
        return m_MainWeapon;
    }

    public virtual void Damaged(Character a_CharacterAttack)
    {
        SetDead(true);
        a_CharacterAttack.OnLevelUp(this);
        StopAllCoroutines();
        OnDead();
    }
    protected virtual void OnDead()
    {
    }
    protected virtual void OnLevelUp(Character a_CharacterAttack)
    {
        m_Level++;
        m_AttackRange += 0.25f;
        m_AttackRange = Mathf.Clamp(m_AttackRange, 0, 14);
        m_CoinClaim += a_CharacterAttack.Level + 1;
    }
    public bool CanAttack()
    {
        Character target = LoadTaget();
        return target != null && !target.IsDead && !m_MainWeapon.IsAttacking();
    }
    public void SetAnimator(CharacterAnimator a_animator)
    {
        m_Animator = a_animator;
    }
    public void SetDead(bool a_isDead)
    {
        m_IsDead = a_isDead;
        m_IsJustDead = a_isDead;
    }
    public void SetCharacterSkin(CharacterSkin skin)
    {
        m_CharacterSkin = skin;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_AttackRange);
    }
}
