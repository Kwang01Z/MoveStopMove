using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : GameState
{
    public void OnEnter(GameController a_Controller)
    {
        a_Controller.SetMainMenuOn(true);
        a_Controller.EnterGame(false);
        a_Controller.MainMenu.Display();
        GameController.Instance.Player.SetHadDeath(false);
    }

    public void OnExit(GameController a_Controller)
    {
        GameController.Instance.MainMenu.Hide();
    }
}
