using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] Animator m_Animator;
    CharacterState m_CurrentState;
    private void Reset()
    {
        m_Animator = GetComponent<Animator>();
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
        m_Animator.SetTrigger(m_CurrentState.ToString());
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
