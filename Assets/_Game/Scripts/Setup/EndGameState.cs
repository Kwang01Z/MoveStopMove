using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameState : GameState
{
    public void OnEnter(GameController a_Controller)
    {
        a_Controller.EndGame(true);
    }

    public void OnExit(GameController a_Controller)
    {
        a_Controller.EndGame(false);
    }
}
