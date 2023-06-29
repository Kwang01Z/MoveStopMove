using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GamePlayUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_AliveCoutText;
    [SerializeField] EnemySpawner m_EnemySpawner;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_AliveCoutText.SetText((m_EnemySpawner.EnemyRemaining+1).ToString());
    }
}
