using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAttack : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.Animator.ChangeState(CharacterState.Attack);
    }

    public void OnExcute(Bot t)
    {
        Character target = t.LoadTaget();
        if (target != null)
        {
            Vector3 dir = target.transform.position - t.transform.position;
            t.RotateObject(dir.normalized);
        }
    }

    public void OnExit(Bot t)
    {
        
    }


}
