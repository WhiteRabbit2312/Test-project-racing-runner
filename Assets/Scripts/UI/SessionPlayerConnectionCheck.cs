using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Linq;
using Zenject;

public class SessionPlayerConnectionCheck : NetworkBehaviour
{
    [SerializeField] private GameObject _informPanel;
    [SerializeField] private DatabaseInfo _databaseInfo;

    [Inject] private DatabaseManager _databaseManager;

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
    
    private async void ShowPlayerInformPanel()
    {
        NetworkObject networkObject = GameStarter.Instance.NetworkRunner.Spawn(_informPanel);

        int idx = 0;
        foreach(var item in GameStarter.Instance.PlayerUserID)
        {
            string name = await _databaseInfo.GetPlayerData(Constants.DatabaseNameKey, item.Value);
            string avatarID = await _databaseInfo.GetPlayerData(Constants.DatabaseAvatarKey, item.Value);

            PreGamePlayersInfoPanel panel = networkObject.GetComponent<PreGamePlayersInfoPanel>();
            int id = int.Parse(avatarID);

            panel.InitPlayer(idx, name, id);
            idx++;
        }

       

        
    }

    
}
