using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayGameButton : MonoBehaviour
{
    [SerializeField] Button m_MainButton;
    private void OnEnable()
    {
        m_MainButton.onClick.AddListener(delegate { StartGame(); });
    }
    private void OnDisable()
    {
        m_MainButton.onClick.RemoveAllListeners();
    }
    void StartGame()
    {
        GameController.Instance.ChangeState(new GamePlayState());
    }
}
