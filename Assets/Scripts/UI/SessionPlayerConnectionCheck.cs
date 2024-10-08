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


    private void OnEnable()
    {
        CheckPlayers(_gameStarter.NetworkRunner);
    }

    public void CheckPlayers(NetworkRunner runner)
    {
        int count = runner.ActivePlayers.Count();

        if (count == Constants.PlayersInSessionCount)
        {
            Debug.LogWarning("���� 2 ������ � ������");
            RPC_ShowPlayerInformPanel();
        }
        else
        {
            //SceneManager.LoadScene(Constants.GameplaySceneIdx);
            SceneRef scene = SceneRef.FromIndex(Constants.GameplaySceneIdx);
            runner.LoadScene(scene);
            Debug.LogWarning($"������� ���������� �������: {count}");
        }
    }

    [Rpc(sources: RpcSources.All, targets: RpcTargets.All)]
    private async void RPC_ShowPlayerInformPanel()
    {
        if (_informPanel == null)
        {
            Debug.LogError("Prefab _informPanel �� �����!");
            return;
        }

        NetworkObject networkObject = _gameStarter.NetworkRunner.Spawn(_informPanel);

        if (networkObject == null)
        {
            Debug.LogError("NetworkObject �� ��� ���������!");
            return;
        }

        PreGamePlayersInfoPanel panel = networkObject.GetComponent<PreGamePlayersInfoPanel>();

        if (panel == null)
        {
            Debug.LogError("��������� PreGamePlayersInfoPanel �� ������ �� ������������ �������!");
            return;
        }

        int idx = 0;
        foreach(var item in _gameStarter.PlayerUserID)
        {
            string name = await _databaseInfo.GetPlayerData(Constants.DatabaseNameKey, item.Value);
            string avatarID = await _databaseInfo.GetPlayerData(Constants.DatabaseAvatarKey, item.Value);

            if (int.TryParse(avatarID, out int id))
            {
                panel.InitPlayer(idx, name, id);
            }
            else
            {
                Debug.LogError("������ �������� avatarID");
            }
            idx++;
        }
    }

    
}
