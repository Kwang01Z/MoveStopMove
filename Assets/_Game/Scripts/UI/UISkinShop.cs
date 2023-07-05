using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UISkinShop : UICanvas
{
    [SerializeField] Button m_ExitButton;
    [SerializeField] Button m_HeadSkinButton;
    [SerializeField] Button m_PantSkinButton;
    [SerializeField] Button m_ShieldSkinButton;
    [SerializeField] Button m_SetFullButton;
    [SerializeField] Button m_PreButton;
    [SerializeField] Button m_NextButton;
    [SerializeField] Button m_SelectButton;
    [SerializeField] Button m_BuyButton;
    [SerializeField] TextMeshProUGUI m_BuyText;
    //SkinType m_CurrentSkinType = SkinType.HeadSkin;
    ISkinShopState m_ShopState;
    public override void Open()
    {
        base.Open();
        UIManager.Instance.OpenUI<UIHeadSkinItem>();
        m_ShopState = new HeadSkinState();
        CameraFollower.Instance.ChangeState(CameraState.Shop);
    }
    public override void Setup()
    {
        base.Setup();
        m_ExitButton.onClick.AddListener(delegate { ExitButtonPressed(); });
        m_HeadSkinButton.onClick.AddListener(delegate { HeadSkinButtonPressed(); });
        m_PantSkinButton.onClick.AddListener(delegate { PantSkinButtonPressed(); });
        m_ShieldSkinButton.onClick.AddListener(delegate { ShieldButtonPressed(); });
        m_SetFullButton.onClick.AddListener(delegate { SetFullButtonPressed(); });
        m_PreButton.onClick.AddListener(delegate { PreButtonPressed(); });
        m_NextButton.onClick.AddListener(delegate { NextButtonPressed(); });
        m_BuyButton.onClick.AddListener(delegate { BuyButtonPressed(); });
        m_SelectButton.onClick.AddListener(delegate { SelectButtonPressed(); });
    }
    void ExitButtonPressed()
    {
        Close();
        UIManager.Instance.OpenUI<MainMenu>();
        SoundManager.Instance.OnPlayButtonClickSound();
    }
    void HeadSkinButtonPressed()
    {
        CloseUIItemAll();
        UIManager.Instance.OpenUI<UIHeadSkinItem>();
        ChangeState(new HeadSkinState());
        SoundManager.Instance.OnPlayButtonClickSound();
    }
    void PantSkinButtonPressed()
    {
        CloseUIItemAll();
        UIManager.Instance.OpenUI<UIPantSkinItem>();
        ChangeState(new PantSkinState());
        SoundManager.Instance.OnPlayButtonClickSound();
    }
    void ShieldButtonPressed()
    {
        CloseUIItemAll();
        UIManager.Instance.OpenUI<UIShieldDisplay>();
        ChangeState(new ShieldSkinState());
        SoundManager.Instance.OnPlayButtonClickSound();
    }
    void SetFullButtonPressed()
    {
        CloseUIItemAll();
        UIManager.Instance.OpenUI<UISetFullDisplay>();
        ChangeState(new SetFullState());
        SoundManager.Instance.OnPlayButtonClickSound();
    }
    void PreButtonPressed()
    {
        m_ShopState.OnChangeItem(-1);
        SoundManager.Instance.OnPlayButtonClickSound();
    }
    void NextButtonPressed()
    {
        m_ShopState.OnChangeItem(1);
        SoundManager.Instance.OnPlayButtonClickSound();
    }
    void SelectButtonPressed()
    {
        m_ShopState.OnSelectItem();
        SoundManager.Instance.OnPlayButtonClickSound();
    }
    void BuyButtonPressed()
    {
        if (SaveManager.Instance.Coin >= m_ShopState.GetItemPrice())
        {
            SoundManager.Instance.OnPlayButtonClickSound();
            m_ShopState.OnBuyItem();
        }
            
    }
    private void Update()
    {
        if (SaveManager.Instance.Coin >= m_ShopState.GetItemPrice())
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
        m_BuyText.SetText(m_ShopState.GetItemPrice().ToString());
        m_SelectButton.gameObject.SetActive(SaveManager.Instance.IsOwnedSkin(m_ShopState.GetItemNameSave()));
        m_BuyButton.gameObject.SetActive(!SaveManager.Instance.IsOwnedSkin(m_ShopState.GetItemNameSave()));
    }
    void CloseUIItemAll()
    {
        UIManager.Instance.CloseUI<UIHeadSkinItem>();
        UIManager.Instance.CloseUI<UIPantSkinItem>();
        UIManager.Instance.CloseUI<UISetFullDisplay>();
        UIManager.Instance.CloseUI<UIShieldDisplay>();
    }
    void ChangeState(ISkinShopState state)
    {
        if (m_ShopState.GetType().Equals(state.GetType())) return;
        m_ShopState = state;
    }
    public override void Close()
    {
        m_ExitButton.onClick.RemoveAllListeners();
        m_HeadSkinButton.onClick.RemoveAllListeners();
        m_PantSkinButton.onClick.RemoveAllListeners();
        m_ShieldSkinButton.onClick.RemoveAllListeners();
        m_SetFullButton.onClick.RemoveAllListeners();
        m_PreButton.onClick.RemoveAllListeners();
        m_NextButton.onClick.RemoveAllListeners();
        m_SelectButton.onClick.RemoveAllListeners();
        m_BuyButton.onClick.RemoveAllListeners();
        CloseUIItemAll();
        CameraFollower.Instance.ChangeState(CameraState.Menu);
        SaveManager.Instance.Save();
        base.Close();
    }
}
