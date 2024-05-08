using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;
using System;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] Player playerModel;
    public readonly SyncVar<string> userName = new();

    public readonly SyncVar<bool> isReady = new();

    Transform _transform;
    Transform _playerTransform;
    private void Awake()
    {
        _transform = transform;
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        RegisterToManager();
    }
    private void Update()
    {
        if (IsOwner)
        {
            _transform.position = _playerTransform.position;
            _transform.rotation = _playerTransform.rotation;
        }
    }

    [ServerRpc]
    public void AssignToPlayer()
    {
        if (IsOwner)
        {
            Player player = Instantiate(playerModel);
            _playerTransform = player.transform;
            ServerSetReady(true);
        }
    }

    [ServerRpc]
    public void RegisterToManager()
    {
        if (!IsOwner) return;
        GameplayNetworkManager.Instance.PlayerNetworkManager.playerList.Add(this);
        AssignToPlayer();
    }

    [ServerRpc]
    public void ServerSetReady(bool isReady)
    {
        if (!IsOwner) return;
        this.isReady.Value = isReady;
    }
}
