using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

public class PlayerSpawner : NetworkBehaviour
{
    [SerializeField] private NetworkObject _player;
    [Inject] private GameStarter _gameStarter;
    private Vector3 _playerStartPosition = new Vector3(0, 1, 0);

    public override void Spawned()
    {
        Debug.LogWarning("My spawned");

        //PlayerRef playerRef = GameStarter.Instance.PlayerUserID.Keys.First();
        Runner.Spawn(_player, _playerStartPosition, Quaternion.identity, GameStarter.Instance.NetworkRunner.LocalPlayer);
    }

}