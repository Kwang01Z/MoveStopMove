using System.Collections;
using System.Collections.Generic;
using FishNet;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayMenu : MonoBehaviour
{
    [SerializeField] Button hostButton;
    [SerializeField] Button clientButton;

    private void Awake()
    {
        hostButton.onClick.AddListener(() =>
        {
            InstanceFinder.ServerManager.StartConnection();
            InstanceFinder.ClientManager.StartConnection();
        });
        clientButton.onClick.AddListener(() =>
        {
            InstanceFinder.ClientManager.StartConnection();
        });
    }
}
