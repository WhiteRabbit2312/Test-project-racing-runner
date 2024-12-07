using System;
using System.Linq;
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
        Debug.LogError("AddPlayerDeath");
        string playerName = PlayerPrefs.GetString(Constants.DatabaseNameKey);
        int score = PlayerSpawner.Instance.Players.FirstOrDefault(a => a.Key == Runner.LocalPlayer).Value.GetComponent<PlayerMovement>().Score;
        Rpc_AddPlayerDeath(playerName, score);
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority, InvokeLocal = true)]
    private void Rpc_AddPlayerDeath(string playerName, int score)
    {
        _destroyedPlayers++;
        Debug.LogError("Destroyed players: " + _destroyedPlayers);
        _finalWindow.AddPlayerData(playerName, score);
        if (_destroyedPlayers == 2)
        {
            _finalWindow.RPC_ShowResults();
        }  
    }
    
}
