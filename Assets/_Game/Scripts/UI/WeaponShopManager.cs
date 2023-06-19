using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WeaponShopManager : MonoBehaviour
{
    [SerializeField] Button m_PreButton;
    [SerializeField] Button m_NextButton;
    [SerializeField] Button m_ExitButton;
    [SerializeField] Button m_BuyButton;
    [SerializeField] Button m_SelectButton;
    [SerializeField] ItemShopDisplay m_Item;
    [SerializeField] TextMeshProUGUI m_NameWeapon;
    [SerializeField] TextMeshProUGUI m_PriceWeapon;
    [SerializeField] MainMenuManager m_Manager;
    [SerializeField] Transform m_IteamCamera;
    private void OnEnable()
    {
        
        m_PreButton.onClick.AddListener(delegate { PreButtonClicked(); });
        m_NextButton.onClick.AddListener(delegate { NextButtonClicked(); });
        m_ExitButton.onClick.AddListener(delegate { GoToMainMenu(); });
        m_BuyButton.onClick.AddListener(delegate { BuyButtonClicked(); });
        m_SelectButton.onClick.AddListener(delegate { SelectButtonClicked(); });
    }
    private void OnDisable()
    {
        m_PreButton.onClick.RemoveAllListeners();
        m_NextButton.onClick.RemoveAllListeners();
        m_ExitButton.onClick.RemoveAllListeners();
        m_BuyButton.onClick.RemoveAllListeners();
        m_SelectButton.onClick.RemoveAllListeners();
    }
    void Update()
    {
        ValidateWeaponInfo();
        DisplaySelectButton(DataPlayer.Instance.IsOwnedWeapon(m_Item.CurrentWeapon.Type));
    }
    void PreButtonClicked()
    {
        m_Item.ChangeWeapon(-1);
    }
    void NextButtonClicked()
    {
        m_Item.ChangeWeapon(1);
    }
    void ValidateWeaponInfo()
    {
        string nameWeapon = m_Item.GetNameCurrentWeapon();
        string priceWeapon = m_Item.CurrentWeapon.Price.ToString();
        m_NameWeapon.SetText(nameWeapon);
        m_PriceWeapon.SetText(priceWeapon);
    }
    void GoToMainMenu()
    {
        m_IteamCamera.gameObject.SetActive(false);
        gameObject.SetActive(false);
        m_Manager.Display();
    }
    void DisplaySelectButton(bool a_bool)
    {
        m_SelectButton.gameObject.SetActive(a_bool);
        m_BuyButton.gameObject.SetActive(!a_bool);
    }
    void BuyButtonClicked()
    {
        if (DataPlayer.Instance.Coin >= m_Item.CurrentWeapon.Price)
        {
            DataPlayer.Instance.AddWeapon(m_Item.CurrentWeapon.Type);
            DataPlayer.Instance.AddCoin(-m_Item.CurrentWeapon.Price);
        }
    }
    void SelectButtonClicked()
    {
        if (!DataPlayer.Instance.EquipedWeapon.Equals(m_Item.CurrentWeapon.Type))
        {
            DataPlayer.Instance.ChangeWeapon(m_Item.CurrentWeapon.Type);
        }
    }
}
