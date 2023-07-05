using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UITotal : UICanvas
{
    [Header("Top Layout")]
    [SerializeField] Slider m_ProgressBar;
    [SerializeField] Image m_PreLevel;
    [SerializeField] Image m_NextLevel;
    [SerializeField] TextMeshProUGUI m_EvaluateText;
    [Header("Mid Layout")]
    [SerializeField] TextMeshProUGUI m_RankText;
    [SerializeField] TextMeshProUGUI m_NotificationText;
    [SerializeField] TextMeshProUGUI m_CoinClampedText;
    [Header("Bottom Layout")]
    [SerializeField] Button m_GotoMenuButton;
    public override void Setup()
    {
        base.Setup();
        m_GotoMenuButton.onClick.RemoveAllListeners();
        m_GotoMenuButton.onClick.AddListener(delegate { GotoMenuButtonClicked(); });
        SetRank();
        SetEvaluteText();
        SetNotification();
        SetCoinClamped(SaveManager.Instance.CoinClaim);
    }
    public override void Open()
    {
        base.Open();
        UIManager.Instance.CloseUI<UIGamePlay>();
        UIManager.Instance.CloseUI<UIJoystick>();
        UIManager.Instance.CloseUI<UIIndicator>();
        UIManager.Instance.CloseUI<UIRevive>();
        LevelManager.Instance.SetHadDeath(false);
        SaveManager.Instance.TotleEndGame();
        SaveManager.Instance.Save();
    }
    public override void Close()
    {
        base.Close();
        
    }
    void GotoMenuButtonClicked()
    {
        Close();
        SoundManager.Instance.OnPlayButtonClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SetCoinClamped(int a_coin)
    {
        m_CoinClampedText.SetText(a_coin.ToString());
    }
    public void SetRank()
    {
        m_RankText.SetText("#" + (LevelManager.Instance.BotRemaining + 1).ToString());
    }
    public void SetEvaluteText()
    {
        if (LevelManager.Instance.BotRemaining > LevelManager.Instance.CharacterAmount / 2)
        {
            m_EvaluateText.SetText("Too bad, try again!");
            SoundManager.Instance.OnPlayLoseSound();
        }
        else if (LevelManager.Instance.BotRemaining > 0)
        {
            SoundManager.Instance.OnPlayLoseSound();
            m_EvaluateText.SetText("You can do it! Let try one more time!");
        }
        else
        {
            SoundManager.Instance.OnPlayWinSound();
            m_EvaluateText.SetText("Congratulations!");
        }
    }
    public void SetNotification()
    {
        if (LevelManager.Instance.BotRemaining > 0)
        {
            m_NotificationText.SetText("You have been died!");
        }
        else
        {
            m_NotificationText.SetText("You are victor");
        }
    }
}
