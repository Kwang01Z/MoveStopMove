using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    GameState m_CurrentState = new MainMenuState();

    [Header("Camera")]
    [SerializeField] FollowPlayer m_CameraMain;
    [Header("Character")]
    [SerializeField] CharacterControllerBase m_Player;
    public CharacterControllerBase Player => m_Player;
    [Header("Joystick")]
    [SerializeField] Transform m_MainJoystick;
    [Header("Indicator")]
    [SerializeField] Transform m_Indicator;
    [SerializeField] Transform m_PlayerIndicator;
    [Header("MainMenu Objects")]
    [SerializeField] MainMenuManager m_MainMenu;
    public MainMenuManager MainMenu => m_MainMenu;
    bool m_IsMainMenuOn = true;
    [Header("Endgame")]
    [SerializeField] EndGameManager m_EndGame;
    public bool IsMainMenuOn => m_IsMainMenuOn;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        ChangeState(new MainMenuState());
    }
    public void ChangeState(GameState a_State)
    {
        if (m_CurrentState.GetType().Equals(a_State.GetType()))  return;
        m_CurrentState.OnExit(this);
        m_CurrentState = a_State;
        m_CurrentState.OnEnter(this);
    }
    public void EnterGame(bool a_bool)
    {
        m_CameraMain.TurnMenu(!a_bool);
        m_MainJoystick.gameObject.SetActive(a_bool);
        m_Indicator.gameObject.SetActive(a_bool);
        m_PlayerIndicator.gameObject.SetActive(a_bool);
    }
    public void SetMainMenuOn(bool a_bool)
    {
        m_IsMainMenuOn = a_bool;
    }
    public void EndGame(bool a_bool)
    {
        m_EndGame.transform.gameObject.SetActive(a_bool);
    }
}
