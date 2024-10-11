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

    public static SessionPlayerConnectionCheck Instance;

    private int Idx { get; set; } = 0;
    private readonly float _showPanelDuration = 5f;

    //[Networked] public NetworkDictionary<PlayerRef, NetworkString<_32>> PlayerUserID => default;
    //public Dictionary<PlayerRef, NetworkString<_32>> PlayerUserID = new Dictionary<PlayerRef, NetworkString<_32>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public override void Spawned()
    {
        int count = GameStarter.Instance.NetRunner.ActivePlayers.Count();

        Debug.LogError("count1 " + count);


        if (count == Constants.PlayersInSessionCount)
        {
            Debug.LogError("count2 " + count);
            RPC_CheckPlayers();
        }
    }

    public void RPC_CheckPlayers()
    {
        RPC_ShowPlayerInformPanel();
        Debug.LogWarning("Есть 2 игрока в сессии");

        foreach (var item in GameStarter.Instance.PlayerUserID)
        {
            if (item.Key.IsMasterClient)
            {
                Debug.LogError("Runner local player");


                RPC_LoadScene();
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
        foreach (var result in GameStarter.Instance.PlayerUserID)
        {
            Debug.LogError("IDX: " + Idx + "result.name: " + result.Key + "result.avatarID: " + result.Value);

            string name = await _databaseInfo.GetPlayerData(Constants.DatabaseNameKey, result.Value.ToString());
            string avatarID = await _databaseInfo.GetPlayerData(Constants.DatabaseAvatarKey, result.Value.ToString());

            RPC_InitPlayerPanel(Idx, name, avatarID);
            Idx++;
        }
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
