using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Linq;
using FishNet;
using FishNet.Broadcast;
using FishNet.Transporting;
using System;

public class GameplayNetworkManager : MonoBehaviour
{
    static GameplayNetworkManager _instance;
    public static GameplayNetworkManager Instance
    {
        get 
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameplayNetworkManager>();
            }
            return _instance;
        }
    }


    [SerializeField] PlayerNetworkManager playerNetworkManager;
    [SerializeField] PlayerNetwork playerNetwork;

    public PlayerNetworkManager PlayerNetworkManager;
    private void Start()
    {
        InstanceFinder.ServerManager.OnServerConnectionState += OnServerConnectionState;
    }

    private void OnServerConnectionState(ServerConnectionStateArgs obj)
    {
        InitManagerNetWorks();
    }

    void InitManagerNetWorks()
    {
        PlayerNetworkManager = Instantiate(playerNetworkManager);
        PlayerNetworkManager._OnStartGame += OnStartGame;
        InstanceFinder.ServerManager.Spawn(PlayerNetworkManager.gameObject);
    }

    void OnStartGame()
    { 
    
    }
}

