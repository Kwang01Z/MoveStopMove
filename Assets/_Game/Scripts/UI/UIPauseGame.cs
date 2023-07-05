using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIPauseGame : UICanvas
{
    [SerializeField] Button m_ResumeButton;
    [SerializeField] Button m_GotoMenuButton;
    public override void Setup()
    {
        base.Setup();
        m_ResumeButton.onClick.RemoveAllListeners();
        m_GotoMenuButton.onClick.RemoveAllListeners();
        m_ResumeButton.onClick.AddListener(delegate { ResumeButtonClicked(); });
        m_GotoMenuButton.onClick.AddListener(delegate { MenuButtonClicked(); });
    }
    public override void Open()
    {
        base.Open();
        Time.timeScale = 0;
        UIManager.Instance.CloseUI<UIJoystick>();
    }
    public override void Close()
    {
        base.Close();
        
        Time.timeScale = 1;
    }
    void ResumeButtonClicked()
    {
        UIManager.Instance.OpenUI<UIJoystick>();
        SoundManager.Instance.OnPlayButtonClickSound();
        Close();
    }
    void MenuButtonClicked()
    {
        Close();
        SoundManager.Instance.OnPlayButtonClickSound();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
