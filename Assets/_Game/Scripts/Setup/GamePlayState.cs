using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : GameState
{
    
    public void OnEnter(GameController a_Controller)
    {
        a_Controller.SetMainMenuOn(false);
        a_Controller.EnterGame(true);
    }

    public void OnExit(GameController a_Controller)
    {
        a_Controller.SetMainMenuOn(true);
        //a_Controller.EnterGame(false);
    }
}
