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
        if (Runner.IsClient)
             PlayerUserID.Set(Runner.LocalPlayer, _databaseManager.FirebaseUser.UserId);

        RPC_CheckPlayers();
    }

   [Rpc(sources: RpcSources.All, targets: RpcTargets.All)]
    public void RPC_CheckPlayers()
    {
        int count = _gameStarter.NetworkRunner.ActivePlayers.Count();

        if (count == Constants.PlayersInSessionCount)
        {
            Debug.LogWarning("Есть 2 игрока в сессии");

            if (_gameStarter.NetworkRunner.LocalPlayer == _gameStarter.NetworkRunner.ActivePlayers.First())
            {
                RPC_ShowPlayerInformPanel();

                StartCoroutine(LoadGameScene());
            }


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
            string valyerString = item.Value.ToString();

            var playerDataTask = RPC_GetPlayerDataAsync(valyerString);
            playerDataTasks.Add(playerDataTask);
        }

        var playerDataResults = await Task.WhenAll(playerDataTasks);

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
