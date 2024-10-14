using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Linq;
using Zenject;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Fusion.Sockets;
using System;

public class SessionPlayerConnectionCheck : NetworkBehaviour
{
    [SerializeField] private GameObject _informPanel;
    [SerializeField] private Image[] _image;
    [SerializeField] private TextMeshProUGUI[] _nick;
    [SerializeField] private AvatarSpriteSO _avatarSpriteSO;

    [Space]
    [SerializeField] private DatabaseInfo _databaseInfo;
    [Inject] private DatabaseManager _databaseManager;

    [Networked]
    public NetworkDictionary<PlayerRef, NetworkString<_32>> PlayerUserID => default;
    private int Idx { get; set; } = 0;
    private readonly float _showPanelDuration = 5f;



    public override void Spawned()
    {
        RPC_InitPlayers();
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void RPC_InitPlayers()
    {
        int count = GameStarter.Instance.NetRunner.ActivePlayers.Count();

        Debug.LogError("count1 " + count);

        if (count == 1)
        {
            IEnumerable<PlayerRef> player = Runner.ActivePlayers;
            PlayerRef playerRef1 = player.FirstOrDefault();
            //Runner.SetMasterClient(playerRef1);
            Debug.LogError("playerRef1 " + playerRef1);

            PlayerUserID.Set(playerRef1, _databaseManager.FirebaseUser.UserId);
        }

        else if (count == Constants.PlayersInSessionCount)
        {

            IEnumerable<PlayerRef> player = Runner.ActivePlayers;
            PlayerRef playerRef1 = player.FirstOrDefault();
            PlayerUserID.Set(playerRef1, _databaseManager.FirebaseUser.UserId);

            Debug.LogError("playerRef2 " + playerRef1);
            RPC_CheckPlayers();
        }
    }

    public void RPC_CheckPlayers()
    {
        RPC_ShowPlayerInformPanel();
        Debug.LogWarning("Есть 2 игрока в сессии");

        foreach (var item in PlayerUserID)
        {
            Debug.LogError("GameStarter.Instance.PlayerUserID: " + PlayerUserID.Count);
            if (item.Key.PlayerId == 1)
            {
                Debug.LogError("Runner local player");


                RPC_LoadScene();
            }

            else
            {
                
                Debug.LogWarning("Player is not master: " + item.Key);
            }
        }
    }

    private void RPC_LoadScene()
    {
        StartCoroutine(LoadGameScene());
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    private async void RPC_ShowPlayerInformPanel()
    {
        Idx = 0;
        foreach (var result in PlayerUserID)
        {
            Debug.LogError("IDX: " + Idx + "result.name: " + result.Key + "result.avatarID: " + result.Value);

            string name = await _databaseInfo.GetPlayerData(Constants.DatabaseNameKey, result.Value.ToString());
            string avatarID = await _databaseInfo.GetPlayerData(Constants.DatabaseAvatarKey, result.Value.ToString());

            RPC_InitPlayerPanel(Idx, name, avatarID);
            Idx++;
        }
        Idx = 0;
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_InitPlayerPanel(int playerID, string nick, string avatarID)
    {
        _informPanel.SetActive(true);
        Debug.LogError("in rpc");
        if (int.TryParse(avatarID, out int id))
        {
            Debug.LogError("playerID: " + playerID + "nick: " + nick + "avatarID: " + avatarID);
            _image[playerID].sprite = _avatarSpriteSO.SpriteAvatar[id];
            _nick[playerID].text = nick;

        }

    }

    private IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(_showPanelDuration);
        SceneRef scene = SceneRef.FromIndex(Constants.GameplaySceneIdx);
        GameStarter.Instance.NetRunner.LoadScene(scene);
    }

}
