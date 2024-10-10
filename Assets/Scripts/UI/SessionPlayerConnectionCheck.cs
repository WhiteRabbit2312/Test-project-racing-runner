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

    [Networked] private int Idx { get; set; } = 0;

    [Networked] private NetworkDictionary<PlayerRef, NetworkString<_32>> PlayerUserID => default;

    public override void Spawned()
    {
        Debug.LogError("UserID: " + _databaseManager.FirebaseUser.UserId);

        
        if (Runner.IsClient)
             PlayerUserID.Set(Runner.LocalPlayer, _databaseManager.FirebaseUser.UserId);
        
        Debug.LogError("UserID in PlsyerUserID: " + PlayerUserID[Runner.LocalPlayer]);

        CheckPlayers(_gameStarter.NetworkRunner);
    }

    public void CheckPlayers(NetworkRunner runner)
    {
        int count = runner.ActivePlayers.Count();

        if (count == Constants.PlayersInSessionCount)
        {
            Debug.LogWarning("≈сть 2 игрока в сессии");

            if(Runner.IsClient)
                RPC_ShowPlayerInformPanel();

            StartCoroutine(LoadGameScene(runner));
        }
        else
        {
            //SceneManager.LoadScene(Constants.GameplaySceneIdx);
            //SceneRef scene = SceneRef.FromIndex(Constants.GameplaySceneIdx);
            //runner.LoadScene(scene);
            Debug.LogWarning($"“екущее количество игроков: {count}");
        }
    }

    [Rpc(sources: RpcSources.All, targets: RpcTargets.All)]
    private async void RPC_ShowPlayerInformPanel()
    {

        //NetworkObject networkObject = _gameStarter.NetworkRunner.Spawn(_informPanel);

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
            Debug.LogError($"name: {result.name} avatarID: {result.avatarID}");
            panel.RPC_InitPlayer(Idx, result.name, result.avatarID);
            Idx++;
        }
    }

    private async Task<(string name, string avatarID)> RPC_GetPlayerDataAsync(string playerID)
    {
        string name = await _databaseInfo.GetPlayerData(Constants.DatabaseNameKey, playerID);
        Debug.LogError("NAME " + name);

        string avatarID = await _databaseInfo.GetPlayerData(Constants.DatabaseAvatarKey, playerID);
        return (name, avatarID);
    }

    private IEnumerator LoadGameScene(NetworkRunner runner)
    {
        yield return new WaitForSeconds(5f);
        SceneRef scene = SceneRef.FromIndex(Constants.GameplaySceneIdx);
        runner.LoadScene(scene);
    }
}
