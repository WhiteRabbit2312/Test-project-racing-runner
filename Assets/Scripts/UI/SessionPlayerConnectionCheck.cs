using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Linq;
using Zenject;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SessionPlayerConnectionCheck : NetworkBehaviour
{
    [SerializeField] private NetworkObject _informPanel;
    [SerializeField] private DatabaseInfo _databaseInfo;

    [Inject] private GameStarter _gameStarter;
    [Inject] private DatabaseManager _databaseManager;

    private int Idx { get; set; } = 0;

    [Networked] private NetworkDictionary<PlayerRef, NetworkString<_32>> PlayerUserID => default;

    public override void Spawned()
    {
        Debug.LogError("UserID: " + _databaseManager.FirebaseUser.UserId);

        
        if (Runner.IsClient)
             PlayerUserID.Set(Runner.LocalPlayer, _databaseManager.FirebaseUser.UserId);
        
        Debug.LogError("UserID in PlsyerUserID: " + PlayerUserID[Runner.LocalPlayer]);

        RPC_CheckPlayers();
    }

   [Rpc(sources: RpcSources.All, targets: RpcTargets.All)]
    public void RPC_CheckPlayers()
    {
        int count = _gameStarter.NetworkRunner.ActivePlayers.Count();

        if (count == Constants.PlayersInSessionCount)
        {
            Debug.LogWarning("≈сть 2 игрока в сессии");

            if (Runner.IsClient)
            {
                
                

            }

            if (_gameStarter.NetworkRunner.LocalPlayer == _gameStarter.NetworkRunner.ActivePlayers.First())
            {
                Debug.LogError("IS SERVER");
                RPC_ShowPlayerInformPanel();

                StartCoroutine(LoadGameScene());

                /*
                SceneRef scene = SceneRef.FromIndex(Constants.GameplaySceneIdx);
                _gameStarter.NetworkRunner.LoadScene(scene);*/
            }
            else
            {
                Debug.LogError("Is not server");
            }

        }
        else
        {
            Debug.LogWarning($"“екущее количество игроков: {count}");
        }
    }

    [Rpc(sources: RpcSources.All, targets: RpcTargets.All)]
    private async void RPC_ShowPlayerInformPanel()
    {
        _informPanel.gameObject.SetActive(true);

        PreGamePlayersInfoPanel panel = _informPanel.GetComponent<PreGamePlayersInfoPanel>();

        List<Task<(string name, string avatarID)>> playerDataTasks = new List<Task<(string name, string avatarID)>>();

        foreach (var item in PlayerUserID)
        {
            // —обираем задачи дл€ получени€ данных каждого игрока

            string valyerString = item.Value.ToString();

            var playerDataTask = RPC_GetPlayerDataAsync(valyerString);
            playerDataTasks.Add(playerDataTask);
        }

        var playerDataResults = await Task.WhenAll(playerDataTasks);

        // »нициализаци€ панели после получени€ всех данных
        foreach (var result in playerDataResults)
        {
            panel.RPC_InitPlayer(Idx, result.name, result.avatarID);
            Idx++;
        }
        Idx = 0;
    }

    private async Task<(string name, string avatarID)> RPC_GetPlayerDataAsync(string playerID)
    {
        string name = await _databaseInfo.GetPlayerData(Constants.DatabaseNameKey, playerID);
        string avatarID = await _databaseInfo.GetPlayerData(Constants.DatabaseAvatarKey, playerID);
        return (name, avatarID);
    }

    private IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(5f);
        SceneRef scene = SceneRef.FromIndex(Constants.GameplaySceneIdx);
        _gameStarter.NetworkRunner.LoadScene(scene);
    }
}
