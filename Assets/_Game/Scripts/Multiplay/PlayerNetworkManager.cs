using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet;
using System;
using System.Linq;

public class PlayerNetworkManager : NetworkBehaviour
{
    public readonly SyncList<PlayerNetwork> playerList = new();
    public readonly SyncVar<bool> allPlayerReady = new();


    public static Action _OnStartGame = null;
    private void Start()
    {
        playerList.OnChange += OnPlayerListChange;
        allPlayerReady.OnChange += OnStartGame;
    }

    private void OnStartGame(bool prev, bool next, bool asServer)
    {
        _OnStartGame?.Invoke();
    }

    private void OnPlayerListChange(SyncListOperation op, int index, PlayerNetwork oldItem, PlayerNetwork newItem, bool asServer)
    {
        CaculateNotiReady();
    }

    [ServerRpc]
    public void CaculateNotiReady()
    {
        allPlayerReady.Value = playerList.All(player => player.isReady.Value);
    }
}
