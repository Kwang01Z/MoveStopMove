using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIWeaponShop : UICanvas
{
    [SerializeField] Button m_ExitButton;
    [SerializeField] Button m_PreButton;
    [SerializeField] Button m_NextButton;
    [SerializeField] Button m_SelectButton;
    [SerializeField] Button m_BuyButton;
    [SerializeField] TextMeshProUGUI m_BuyText;
    [SerializeField] TextMeshProUGUI m_NameItemText;
    WeaponInfo weaponInfo;
    public override void Open()
    {
        base.Open();
        UIManager.Instance.OpenUI<UIWeaponItem>();
    }
    public override void Setup()
    {
        base.Setup();
        m_ExitButton.onClick.RemoveAllListeners();
        m_PreButton.onClick.RemoveAllListeners();
        m_NextButton.onClick.RemoveAllListeners();
        m_SelectButton.onClick.RemoveAllListeners();
        m_BuyButton.onClick.RemoveAllListeners();
        m_ExitButton.onClick.AddListener(delegate { ExitButtonPressed(); });
        m_PreButton.onClick.AddListener(delegate { PreButtonPressed(); });
        m_NextButton.onClick.AddListener(delegate { NextButtonPressed(); });
        m_SelectButton.onClick.AddListener(delegate { SelectButtonPressed(); });
        m_BuyButton.onClick.AddListener(delegate { BuyButtonPressed(); });
    }
    void ExitButtonPressed()
    {
        Close();
        UIManager.Instance.OpenUI<MainMenu>();
        SoundManager.Instance.OnPlayButtonClickSound();
    }
    void PreButtonPressed()
    {
        UIWeaponItem.Instance.ChangeWeapon(-1);
        SoundManager.Instance.OnPlayButtonClickSound();
    }
    void NextButtonPressed()
    {
        UIWeaponItem.Instance.ChangeWeapon(1);
        SoundManager.Instance.OnPlayButtonClickSound();
    }
    void SelectButtonPressed()
    {
        SaveManager.Instance.SelectWeapon(weaponInfo.SaveTxt);
        SoundManager.Instance.OnPlayButtonClickSound();
    }
    void BuyButtonPressed()
    {
        if (weaponInfo.Price <= SaveManager.Instance.Coin)
        {
            SaveManager.Instance.AddCoin(-weaponInfo.Price);
            SaveManager.Instance.AddWeapon(weaponInfo.SaveTxt);
            SoundManager.Instance.OnPlayButtonClickSound();
        }
    }
    private void Update()
    {
        weaponInfo = UIWeaponItem.Instance.GetCurrentWeaponInfo();
        if (weaponInfo.Price <= SaveManager.Instance.Coin)
        {
            Color color;
            if (ColorUtility.TryParseHtmlString("#006C00", out color))
            {
                m_BuyText.color = color;
            }
            else
            {
                Debug.LogWarning("cant change buy color");
            }
        }
        else
        {
            m_BuyText.color = Color.red;
        }
        m_BuyText.SetText(weaponInfo.Price.ToString());
        m_NameItemText.SetText(weaponInfo.Name);
        m_SelectButton.gameObject.SetActive(SaveManager.Instance.IsOwnedWeapon(weaponInfo.SaveTxt));
        m_BuyButton.gameObject.SetActive(!SaveManager.Instance.IsOwnedWeapon(weaponInfo.SaveTxt));
    }
    public override void Close()
    {
        
        UIManager.Instance.CloseUI<UIWeaponItem>();
        SaveManager.Instance.Save();
        base.Close();
    }
}
