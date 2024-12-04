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
    private Vector3 _playerStartPosition = new Vector3(0, 2, 0);

    [Networked] 
    public NetworkDictionary<PlayerRef, NetworkObject> Players => default;
    public override void Spawned()
    {
        Debug.LogWarning("My spawned");

        //PlayerRef playerRef = GameStarter.Instance.PlayerUserID.Keys.First();
        if (Runner == null)
        {
            Debug.LogWarning("Runner is null");
        }

        if (GameStarter.Instance.NetRunner.LocalPlayer == null)
        {
            Debug.LogWarning("Local player is null");

        }

        if (GameStarter.Instance.NetRunner == null)
        {
            Debug.LogWarning(" GameStarter.Instance.NetRunner is null");
        }


        if (Runner.IsClient)
        {
            var player = Runner.Spawn(_player, _playerStartPosition, Quaternion.identity, GameStarter.Instance.NetRunner.LocalPlayer);
            Players.Add(Runner.LocalPlayer, player);
            Debug.LogError("Players count: " + Players.Count);
        }

        else
        {
            Debug.LogError("runner is not client");
        }
    }

}