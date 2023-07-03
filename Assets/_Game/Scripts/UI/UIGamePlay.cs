using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIGamePlay : UICanvas
{
    [SerializeField] TextMeshProUGUI m_AliveAmountText;
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
}
