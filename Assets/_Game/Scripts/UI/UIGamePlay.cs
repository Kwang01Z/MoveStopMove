using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI m_AliveAmountText;
    [SerializeField] Button m_PauseGameButton;
    public override void Setup()
    {
        base.Setup();
        m_PauseGameButton.onClick.RemoveAllListeners();
        m_PauseGameButton.onClick.AddListener(delegate { PauseGameButtonCliked(); });
    }
    public override void Open()
    {
        base.Open();
        GameManager.Instance.ChangeState(GameState.Gameplay);
        UIManager.Instance.OpenUI<UIIndicator>();
        UIManager.Instance.OpenUI<UIJoystick>();
        CameraFollower.Instance.ChangeState(CameraState.GamePlay);
    }
    private void Update()
    {
        m_AliveAmountText.SetText((LevelManager.Instance.BotRemaining+1).ToString());
    }
    public override void Close()
    {
        base.Close();
        
    }
    void PauseGameButtonCliked()
    {
        UIManager.Instance.OpenUI<UIPauseGame>();
        SoundManager.Instance.OnPlayButtonClickSound();
    }
}
