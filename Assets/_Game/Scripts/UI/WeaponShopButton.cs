using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeaponShopButton : MonoBehaviour
{
    [SerializeField] Button m_MainButton;
    [SerializeField] MainMenuManager m_Manager;
    [SerializeField] Transform m_WeaponShopLayer;
    [SerializeField] Transform m_ItemCamera;
    [SerializeField] Transform m_CoinDisplay;
    private void OnEnable()
    {
        m_MainButton.onClick.AddListener(delegate { DisplayWeaponShop(); });
    }
    private void OnDisable()
    {
        m_MainButton.onClick.RemoveAllListeners();
    }
    public void DisplayWeaponShop()
    {
        m_Manager.Hide();
        m_WeaponShopLayer.gameObject.SetActive(true);
        m_ItemCamera.gameObject.SetActive(true);
        m_CoinDisplay.gameObject.SetActive(true);
    }
}
