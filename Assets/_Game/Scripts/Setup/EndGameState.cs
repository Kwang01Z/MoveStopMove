using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameState : GameState
{
    public void OnEnter(GameController a_Controller)
    {
        if (a_Controller.CanEndGame())
        {
            a_Controller.EndGame(true);
        }
        else
        {
            a_Controller.Revive(true);
        }
    }

    public void OnExit(GameController a_Controller)
    {
        a_Controller.Revive(false);
        a_Controller.EndGame(false);
    }
}
