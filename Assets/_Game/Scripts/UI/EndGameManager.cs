using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class EndGameManager : MonoBehaviour
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
    [Header("Other")]
    [SerializeField] EnemySpawner m_EnemySpawner;
    public EnemySpawner Spawner => m_EnemySpawner;
    [SerializeField] DataPlayer m_DataPlayer;
    private void OnEnable()
    {
        m_GotoMenuButton.onClick.AddListener(delegate { GotoMenuButtonClicked(); });
    }
    private void OnDisable()
    {
        m_GotoMenuButton.onClick.RemoveAllListeners();
    }
    void GotoMenuButtonClicked()
    {
        m_DataPlayer.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SetCoinClamped(int a_coin)
    {
        m_CoinClampedText.SetText(a_coin.ToString());
    }
    public void SetRank()
    {
        m_RankText.SetText("#"+(m_EnemySpawner.EnemyRemaining+1).ToString());
    }
    public void SetEvaluteText()
    {
        if (m_EnemySpawner.EnemyRemaining > m_EnemySpawner.EnemyCount / 2)
        {
            m_EvaluateText.SetText("Too bad, try again!");
        }
        else if (m_EnemySpawner.EnemyRemaining > 0)
        {
            m_EvaluateText.SetText("You can do it! Let try one more time!");
        }
        else
        {
            m_EvaluateText.SetText("Congratulations!");
        }
    }
    public void SetNotification()
    {
        if (m_EnemySpawner.EnemyRemaining > 0)
        {
            m_NotificationText.SetText("You have been died!");
        }
        else
        {
            m_NotificationText.SetText("You are victor");
        }
    }
}
