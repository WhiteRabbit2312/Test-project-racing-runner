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
        GameStarter.Instance.NetworkRunner.Spawn(_player);
    }
}