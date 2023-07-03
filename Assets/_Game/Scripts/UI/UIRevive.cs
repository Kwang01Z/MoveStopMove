using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIRevive : UICanvas
{
    [SerializeField] Button m_ExitButon;
    [SerializeField] Transform m_CountDownCircle;
    [SerializeField] Button m_RebornButton;
    [SerializeField] TextMeshProUGUI m_CountdownText;
    [SerializeField] TextMeshProUGUI m_RebornPriceText;
    float rotationAngle = 0f;
    int m_CountDown;
    bool m_IsCountDowning;
    public override void Setup()
    {
        base.Setup();
        m_CountDown = 5;
        m_ExitButon.onClick.AddListener(delegate { ExitButtonClicked(); });
        m_RebornButton.onClick.AddListener(delegate { RebornButtonClicked(); });
    }
    public override void Open()
    {
        base.Open();
        LevelManager.Instance.SetHadDeath(true);
        GameManager.Instance.ChangeState(GameState.EndGame);
        UIManager.Instance.CloseUI<UIJoystick>();
        UIManager.Instance.CloseUI<UIIndicator>();
    }
    public override void Close()
    {
        base.Close();
        m_ExitButon.onClick.RemoveAllListeners();
        m_RebornButton.onClick.RemoveAllListeners();
    }
    void ExitButtonClicked()
    {
        UIManager.Instance.OpenUI<UITotal>();
    }
    void RebornButtonClicked()
    {
        if (SaveManager.Instance.Coin < 200) return;
        SaveManager.Instance.Reborn();
        UIManager.Instance.OpenUI<UIGamePlay>();
        Close();
    }
    private void Update()
    {
        rotationAngle -= 8;
        m_CountDownCircle.localRotation = Quaternion.Euler(0, 0, rotationAngle);
        if (m_CountDown > 0)
        {
            m_CountdownText.SetText(m_CountDown.ToString());
            if (!m_IsCountDowning)
            {
                m_IsCountDowning = true;
                StartCoroutine(CountDownSystem());
            }
        }
    }
    IEnumerator CountDownSystem()
    {
        yield return new WaitForSeconds(1);
        m_CountDown--;
        m_IsCountDowning = false;
        if (m_CountDown <= 0)
        {
            ExitButtonClicked();
        }
    }
}
