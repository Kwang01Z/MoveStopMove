using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ReviveManager : MonoBehaviour
{
    [SerializeField] Button m_ExitButon;
    [SerializeField] Transform m_CountDownCircle;
    [SerializeField] Button m_RebornButton;
    [SerializeField] TextMeshProUGUI m_CountdownText;
    [SerializeField] EndGameManager m_EndGame;
    float rotationAngle = 0f;
    int m_CountDown;
    bool m_IsCountDowning;
    private void OnEnable()
    {
        m_CountDown = 5;
        m_ExitButon.onClick.AddListener(delegate { ExitButtonClicked(); });
        m_RebornButton.onClick.AddListener(delegate { RebornButtonClicked(); });
    }
    private void OnDisable()
    {
        m_ExitButon.onClick.RemoveAllListeners();
        m_RebornButton.onClick.RemoveAllListeners();
    }
    void ExitButtonClicked()
    {
        gameObject.SetActive(false);
        m_EndGame.gameObject.SetActive(true);
    }
    void RebornButtonClicked()
    {
        if (DataPlayer.Instance.Coin < 200) return;
        DataPlayer.Instance.AddCoin(-200);
        GameController.Instance.Player.SetHadDeath(true);
        GameController.Instance.ChangeState(new GamePlayState());
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
