using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainMenu : UICanvas
{
    [Header("Buttons")]
    [SerializeField] Button m_PlayGameButton;
    [SerializeField] Button m_WeaponShopButton;
    [SerializeField] Button m_SkinShopButton;
    [Header("Level")]
    [SerializeField] Image m_LevelImage;
    [SerializeField] Slider m_LevelSlider;
    [Header("Data")]
    [SerializeField] List<Sprite> m_LevelImageData;
    // Others
    int m_LevelPlayer = 1;
    public override void Setup()
    {
        base.Setup();
        m_PlayGameButton.onClick.AddListener(delegate { PlayGameButtonPressed(); });
        m_WeaponShopButton.onClick.AddListener(delegate { WeaponShopButtonPressed(); });
        m_SkinShopButton.onClick.AddListener(delegate { SkinShopButtonPressed(); });
        m_LevelSlider.value = GetLevelProgressRatio();
        m_LevelImage.sprite = GetLevelImage();
    }
    public override void Open()
    {
        base.Open();
        UIManager.Instance.OpenUI<UICoin>();
    }
    public override void Close()
    {
        base.Close();
        m_PlayGameButton.onClick.RemoveAllListeners();
        m_WeaponShopButton.onClick.RemoveAllListeners();
        m_SkinShopButton.onClick.RemoveAllListeners();
    }
    void PlayGameButtonPressed()
    {
        UIManager.Instance.CloseUI<UICoin>();
        Close();
        UIManager.Instance.OpenUI<UIGamePlay>();
        SoundManager.Instance.OnPlayButtonClickSound();
    }
    void WeaponShopButtonPressed()
    {
        Close();
        UIManager.Instance.OpenUI<UIWeaponShop>();
        SoundManager.Instance.OnPlayButtonClickSound();
    }
    void SkinShopButtonPressed()
    {
        Close();
        UIManager.Instance.OpenUI<UISkinShop>();
        SoundManager.Instance.OnPlayButtonClickSound();
    }
    float GetLevelProgressRatio()
    {
        return 0;
    }
    Sprite GetLevelImage()
    {
        return m_LevelImageData[m_LevelPlayer-1];
    }
}
