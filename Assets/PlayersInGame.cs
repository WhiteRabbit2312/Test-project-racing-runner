using System;
using Fusion;
using UnityEngine;

public class PlayersInGame : NetworkBehaviour
{
    [SerializeField] private FinalWindow _finalWindow;
    
    [Networked] private int _destroyedPlayers { get; set; }

    public override void Spawned()
    {
        PlayerMovement.OnPlayerDeath += AddPlayerDeath;
    }

    private void AddPlayerDeath()
    {
        Rpc_AddPlayerDeath();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority, InvokeLocal = true)]
    private void Rpc_AddPlayerDeath()
    {
        _destroyedPlayers++;
        Debug.LogError("Destroyed players: " + _destroyedPlayers);
        if (_destroyedPlayers == 2)
        {
            _finalWindow.RPC_ShowResults();
        }  
    }
    
    private void CheckPlayers()
    {
        int count = 0;
        foreach (var item in PlayerSpawner.Instance.Players)
        {
            if (item.Value.GetComponent<PlayerMovement>().IsAlive)
            {
                count++;
            }
        }

        if (count == 2)
        {
            _finalWindow.RPC_ShowResults();
        }
    }
}
