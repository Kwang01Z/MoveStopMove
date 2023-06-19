using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Transform m_LevelDisplay;
    [SerializeField] Transform m_CoinDisplay;
    [SerializeField] Transform m_AdsButton;
    [SerializeField] Transform m_VibrateButton;
    [SerializeField] Transform m_SoundButton;
    [SerializeField] Transform m_PlayGameButton;
    [SerializeField] Transform m_WeaponShopButton;
    [SerializeField] Transform m_SkinShopButton;
    public void Display()
    {
        m_LevelDisplay.gameObject.SetActive(true);
        m_CoinDisplay.gameObject.SetActive(true);
        m_AdsButton.gameObject.SetActive(true);
        m_VibrateButton.gameObject.SetActive(true);
        m_SoundButton.gameObject.SetActive(true);
        m_PlayGameButton.gameObject.SetActive(true);
        m_WeaponShopButton.gameObject.SetActive(true);
        m_SkinShopButton.gameObject.SetActive(true);
    }
    public void Hide()
    {
        m_LevelDisplay.gameObject.SetActive(false);
        m_CoinDisplay.gameObject.SetActive(false);
        m_AdsButton.gameObject.SetActive(false);
        m_VibrateButton.gameObject.SetActive(false);
        m_SoundButton.gameObject.SetActive(false);
        m_PlayGameButton.gameObject.SetActive(false);
        m_WeaponShopButton.gameObject.SetActive(false);
        m_SkinShopButton.gameObject.SetActive(false);
    }
}
