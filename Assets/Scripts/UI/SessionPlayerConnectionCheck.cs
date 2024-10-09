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
    [SerializeField] private GameObject _informPanel;
    [SerializeField] private DatabaseInfo _databaseInfo;

    [Inject] private GameStarter _gameStarter;
    [Inject] private DatabaseManager _databaseManager;


    public override void Spawned()
    {
        if (Runner.IsServer)
            GameStarter.Instance.PlayerUserID.Add(GameStarter.Instance.NetworkRunner.LocalPlayer, _databaseManager.FirebaseUser.UserId);
        CheckPlayers(_gameStarter.NetworkRunner);
    }

    public void CheckPlayers(NetworkRunner runner)
    {
        int count = runner.ActivePlayers.Count();

        if (count == Constants.PlayersInSessionCount)
        {
            Debug.LogWarning("Есть 2 игрока в сессии");
            RPC_ShowPlayerInformPanel();
        }
        else
        {
            //SceneManager.LoadScene(Constants.GameplaySceneIdx);
            //SceneRef scene = SceneRef.FromIndex(Constants.GameplaySceneIdx);
            //runner.LoadScene(scene);
            Debug.LogWarning($"Текущее количество игроков: {count}");
        }
    }

    [Rpc(sources: RpcSources.All, targets: RpcTargets.All)]
    private async void RPC_ShowPlayerInformPanel()
    {
        Debug.LogError("1");
        NetworkObject networkObject = _gameStarter.NetworkRunner.Spawn(_informPanel);
        Debug.LogError("2");

        PreGamePlayersInfoPanel panel = networkObject.GetComponent<PreGamePlayersInfoPanel>();
        if (Runner.IsServer)
            Debug.LogError("3: " + GameStarter.Instance.PlayerUserID.Count);

        int idx = 0;
        if (Runner.IsServer)
        {
            foreach (var item in _gameStarter.PlayerUserID)
            {
                string name = await _databaseInfo.GetPlayerData(Constants.DatabaseNameKey, item.Value);
                string avatarID = await _databaseInfo.GetPlayerData(Constants.DatabaseAvatarKey, item.Value);

                if (int.TryParse(avatarID, out int id))
                {
                    panel.InitPlayer(idx, name, id);
                }
                else
                {
                    Debug.LogError("Ошибка парсинга avatarID");
                }
                idx++;
            }
        }
    }

    
}
