using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SkinShopManager : MonoBehaviour
{
    [Header("Skin Type Button")]
    [SerializeField] Button m_HeadSkinButton;
    [SerializeField] Button m_PantSkinButton;
    [SerializeField] Button m_BodySkinButton;
    [SerializeField] Button m_ClotherSkinButton;
    [Header("Function Button")]
    [SerializeField] Button m_PreButton;
    [SerializeField] Button m_NextButton;
    [SerializeField] Button m_SelectButton;
    [SerializeField] Button m_BuyButton;
    [Header("Other")]
    [SerializeField] Button m_ExitButton;
    [SerializeField] TextMeshProUGUI m_DescriptionSkin;
    [SerializeField] TextMeshProUGUI m_PriceSkin;
    [SerializeField] MainMenuManager m_Manager;
    [SerializeField] Transform m_IteamCamera;
    [SerializeField] SkinShopDisplay m_SkinShopDisplay;
    [SerializeField] CharacterControllerBase m_Character;
    private void OnEnable()
    {
        m_HeadSkinButton.onClick.AddListener(delegate { HeadSkinButtonClicked(); });
        m_PantSkinButton.onClick.AddListener(delegate { PantSkinButtonClicked(); });
        m_BodySkinButton.onClick.AddListener(delegate { BodySkinButtonClicked(); });
        m_ClotherSkinButton.onClick.AddListener(delegate { ClotherSkinButtonClicked(); });
        m_PreButton.onClick.AddListener(delegate { PreSkinButtonClicked(); });
        m_NextButton.onClick.AddListener(delegate { NextSkinButtonClicked(); });
        m_SelectButton.onClick.AddListener(delegate { SelectSkinButtonClicked(); });
        m_BuyButton.onClick.AddListener(delegate { BuySkinButtonClicked(); });
        m_ExitButton.onClick.AddListener(delegate { ExitButtonClick(); });
    }
    private void OnDisable()
    {
        m_HeadSkinButton.onClick.RemoveAllListeners();
        m_PantSkinButton.onClick.RemoveAllListeners();
        m_BodySkinButton.onClick.RemoveAllListeners();
        m_PreButton.onClick.RemoveAllListeners();
        m_ClotherSkinButton.onClick.RemoveAllListeners();
        m_NextButton.onClick.RemoveAllListeners();
        m_SelectButton.onClick.RemoveAllListeners();
        m_BuyButton.onClick.RemoveAllListeners();
        m_ExitButton.onClick.RemoveAllListeners();
    }
    private void Update()
    {
        m_PriceSkin.SetText(m_SkinShopDisplay.GetCurrentItemPrice().ToString());
        DisplaySelectButton(DataPlayer.Instance.IsOwnedSkin(m_SkinShopDisplay.CurrentSkinType, m_SkinShopDisplay.GetCurrentItemName()));
    }
    void HeadSkinButtonClicked()
    {
        m_SkinShopDisplay.ChangeSkinType(SkinType.HeadSkin);
    }
    void PantSkinButtonClicked()
    {
        m_SkinShopDisplay.ChangeSkinType(SkinType.Pant);
    }
    void BodySkinButtonClicked() { }
    void ClotherSkinButtonClicked() { }
    void PreSkinButtonClicked() 
    {
        m_SkinShopDisplay.ChangeSkinIndex(-1);
    }
    void NextSkinButtonClicked() 
    {
        m_SkinShopDisplay.ChangeSkinIndex(1);
    }
    void SelectSkinButtonClicked() 
    {
        KeyValuePair<SkinType,string> currentSkin = new KeyValuePair<SkinType, string>(m_SkinShopDisplay.CurrentSkinType, m_SkinShopDisplay.GetCurrentItemName());
        if (!DataPlayer.Instance.CurrentSkin.Equals(currentSkin))
        {
            DataPlayer.Instance.ChangeSkin(currentSkin);
        }
        m_SkinShopDisplay.SetSkinForChar(m_Character.CharacterSkin.GetCharacterSkin);
    }
    void DisplaySelectButton(bool a_bool)
    {
        m_SelectButton.gameObject.SetActive(a_bool);
        m_BuyButton.gameObject.SetActive(!a_bool);
    }
    void BuySkinButtonClicked() 
    {
        if (DataPlayer.Instance.Coin >= m_SkinShopDisplay.GetCurrentItemPrice())
        {
            DataPlayer.Instance.AddSkin(m_SkinShopDisplay.CurrentSkinType, m_SkinShopDisplay.GetCurrentItemName());
            DataPlayer.Instance.AddCoin(-m_SkinShopDisplay.GetCurrentItemPrice());
        }
    }
    void ExitButtonClick()
    {
        m_Manager.Display();
        m_IteamCamera.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
