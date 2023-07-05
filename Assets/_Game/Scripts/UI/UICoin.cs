using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UICoin : UICanvas
{
    [SerializeField] TextMeshProUGUI m_CoinText;
    [SerializeField] Button m_AddCoinButton;
    void Update()
    {
        m_CoinText.SetText(SaveManager.Instance.Coin.ToString());
    }
    public override void Setup()
    {
        base.Setup();
        m_AddCoinButton.onClick.RemoveAllListeners();
        m_AddCoinButton.onClick.AddListener(delegate { AddCoin(); });
    }
    public override void Close()
    {
        base.Close();
        
    }
    void AddCoin()
    {
        SaveManager.Instance.AddCoin(100);
    }
}
