using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotRun : IState<Bot>
{
    Vector3 m_NextPos;
    float m_TimeToRun;
    public void OnEnter(Bot t)
    {
        m_NextPos = t.GetRandomNavMeshPosition();
        t.Agent.isStopped = false;
        t.Agent.SetDestination(m_NextPos);
        t.Animator.ChangeState(CharacterState.Run);
        m_TimeToRun = 0;
    }

    public void OnExcute(Bot t)
    {
        m_TimeToRun += Time.deltaTime;
        if (t.CanAttack())
        {
            t.ChageState(new BotIdle());
        }
        if (Vector3.Distance(t.transform.position, m_NextPos) < 0.5f || 
            m_TimeToRun > 5f)
        {
            t.ChageState(new BotIdle());
        }
        Vector3 dir = m_NextPos - t.transform.position;
        t.Animator.transform.rotation = Quaternion.LookRotation(dir, t.transform.up);
    }

    public void OnExit(Bot t)
    {
        t.Agent.isStopped = true;
    }
}
