using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] Animator m_Animator;
    [SerializeField] Character m_Character;
    CharacterState m_CurrentState;
    private void Reset()
    {
        m_Animator = GetComponent<Animator>();
        m_Character = GetComponentInParent<Character>();
    }
    private void Awake()
    {
        m_Character.SetAnimator(this);
    }
    void Start()
    {
        ChangeState(CharacterState.Idle);
    }

    public void ChangeState(CharacterState a_State)
    {
        if (a_State.Equals(m_CurrentState)) return;
        m_Animator.ResetTrigger(m_CurrentState.ToString());
        m_CurrentState = a_State;
        m_Animator.SetTrigger(a_State.ToString());
    }
    public void OnAttack()
    {
        m_Character.OnAttack();
    }
    public void EndAttack()
    {
        m_Character.EndAttack();
    }
}
public enum CharacterState
{
    Idle,
    Run,
    Dance,
    DanceWin,
    Attack,
    Ulti,
    Dead
}
