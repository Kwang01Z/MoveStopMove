using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UICoin : UICanvas
{
    [SerializeField] TextMeshProUGUI m_CoinText;
    void Update()
    {
        m_CoinText.SetText(SaveManager.Instance.Coin.ToString());
    }
}
