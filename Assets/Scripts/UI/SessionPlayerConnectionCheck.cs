using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Linq;
using UnityEngine.SceneManagement;

public class SessionPlayerConnectionCheck : NetworkBehaviour
{
    [SerializeField] private GameObject _informPanel;

    private void OnEnable()
    {
        CheckPlayers(GameStarter.Instance.NetworkRunner);
    }

    public void CheckPlayers(NetworkRunner runner)
    {
        int count = runner.ActivePlayers.Count();

        if (count == Constants.PlayersInSessionCount)
        {
            Debug.LogWarning("Есть 2 игрока в сессии");
            ShowPlayerInformPanel();
        }
        else
        {
            Debug.LogWarning($"Текущее количество игроков: {count}");
        }
    }
    
    private void ShowPlayerInformPanel()
    {
        GameStarter.Instance.NetworkRunner.Spawn(_informPanel);
    }
}
