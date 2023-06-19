using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoinDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_CoinText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_CoinText.SetText(DataPlayer.Instance.Coin.ToString());
    }
}
