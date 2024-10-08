using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] private NetworkObject _player;
    [Inject] private GameStarter _gameStarter;
    private void Awake()
    {
        if (Runner == null)
        {
            Debug.LogWarning("Runner is null");
        }
    }
    

    public override void Spawned()
    {
        Debug.LogWarning("My spawned");
        _gameStarter.NetworkRunner.Spawn(_player);
    }

}