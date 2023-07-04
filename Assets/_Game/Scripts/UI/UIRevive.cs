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
    AudioClip evenSound;
    AudioClip oddSound;
    float rotationAngle = 0f;
    int m_CountDown;
    bool m_IsCountDowning;
    public override void Setup()
    {
        base.Setup();
        evenSound = Resources.Load<AudioClip>("Sounds/count_down");
        oddSound = Resources.Load<AudioClip>("Sounds/count_down2");
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
        SoundManager.Instance.OnPlayButtonClickSound();
    }
    void RebornButtonClicked()
    {
        if (SaveManager.Instance.Coin < 200) return;
        SaveManager.Instance.Reborn();
        UIManager.Instance.OpenUI<UIGamePlay>();
        SoundManager.Instance.OnPlayButtonClickSound();
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
        if (SaveManager.Instance.Coin >= 200)
        {
            Color color;
            if (ColorUtility.TryParseHtmlString("#006C00", out color))
            {
                m_RebornPriceText.color = color;
            }
            else
            {
                Debug.LogWarning("cant change buy color");
            }
        }
        else
        {
            m_RebornPriceText.color = Color.red;
        }
    }
    IEnumerator CountDownSystem()
    {
        if (m_CountDown % 2 == 0)
        {
            SoundManager.Instance.OnPlayAudioClip(evenSound);
        }
        else
        {
            SoundManager.Instance.OnPlayAudioClip(oddSound);
        }
        yield return new WaitForSeconds(1);
        m_CountDown--;
        m_IsCountDowning = false;
        if (m_CountDown <= 0)
        {
            ExitButtonClicked();
        }
    }
}
