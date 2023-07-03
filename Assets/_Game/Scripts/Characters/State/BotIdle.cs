using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotIdle : IState<Bot>
{
    float m_TimeIdle;
    public void OnEnter(Bot t)
    {
        t.Animator.ChangeState(CharacterState.Idle);
        m_TimeIdle = 0;
    }

    public void OnExcute(Bot t)
    {
        m_TimeIdle += Time.deltaTime;
        if (t.CanAttack() && m_TimeIdle> 1f)
        {
            t.ChageState(new BotAttack());
        }
        if (m_TimeIdle > t.TimeIdle)
        {
            t.ChageState(new BotRun());
        }
    }

    public void OnExit(Bot t)
    {
    }

}
