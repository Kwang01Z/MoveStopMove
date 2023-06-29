using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BuyButton : MonoBehaviour
{
    [SerializeField] int m_Price;
    [SerializeField] TextMeshProUGUI m_PriceText;
    [SerializeField] Color m_EnoughMoneyColor;
    [SerializeField] Color m_NotEnoughMoneyColor;
    bool m_IsEnoughMoney;
    void Update()
    {
        m_IsEnoughMoney = DataPlayer.Instance.Coin >= m_Price;
        m_PriceText.color = m_IsEnoughMoney ? m_EnoughMoneyColor : m_NotEnoughMoneyColor;
    }
}
