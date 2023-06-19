using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GameState
{
    public void OnEnter(GameController a_Controller);
    public void OnExit(GameController a_Controller);
}
