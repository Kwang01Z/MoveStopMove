using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkinShopButton : MonoBehaviour
{
    [SerializeField] Button m_MainButton;
    [SerializeField] MainMenuManager m_Manager;
    [SerializeField] Transform m_SkinShopLayer;
    [SerializeField] Transform m_SkinCamera;
    private void OnEnable()
    {
        m_MainButton.onClick.AddListener(delegate { DisplaySkinShop(); });
    }
    private void OnDisable()
    {
        m_MainButton.onClick.RemoveAllListeners();
    }
    public void DisplaySkinShop()
    {
        m_Manager.Hide();
        m_SkinShopLayer.gameObject.SetActive(true);
        m_SkinCamera.gameObject.SetActive(true);
    }
}
