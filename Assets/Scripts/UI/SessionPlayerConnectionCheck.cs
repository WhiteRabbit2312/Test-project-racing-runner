using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Linq;
using Zenject;
using UnityEngine.UI;
using TMPro;

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
    private readonly float _showPanelDuration = 5f;

    public override void Spawned()
    {
        InitPlayers();  
    }

    private void InitPlayers()
    {
        int count = GameStarter.Instance.NetRunner.ActivePlayers.Count();
            
        if (count == Constants.FirstPlayerID)
        {
            SetPlayer();
        }

        else if (count == Constants.PlayersInSessionCount)
            //if(count == 1)
        {

            SetPlayer();

            ShowPlayerInformPanel();
            RPC_LoadScene();
        }
    }

    private void SetPlayer()
    {
        IEnumerable<PlayerRef> player = Runner.ActivePlayers;
        PlayerRef playerRef1 = player.FirstOrDefault();
        PlayerUserID.Set(playerRef1, _databaseManager.FirebaseUser.UserId);
    }

    [Rpc(RpcSources.All, RpcTargets.All)]

    private void RPC_LoadScene()
    {
        if(Runner.LocalPlayer.PlayerId == Constants.FirstPlayerID)
            StartCoroutine(LoadGameScene());
    }

    private async void ShowPlayerInformPanel()
    {
        int idx = 0;

        foreach (var result in PlayerUserID)
        {
            string name = await _databaseInfo.GetPlayerData(Constants.DatabaseNameKey, result.Value.ToString());
            string avatarID = await _databaseInfo.GetPlayerData(Constants.DatabaseAvatarKey, result.Value.ToString());

            RPC_InitPlayerPanel(idx, name, avatarID);
            idx++;
        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_InitPlayerPanel(int playerID, string nick, string avatarID)
    {
        _informPanel.SetActive(true);
        if (int.TryParse(avatarID, out int id))
        {
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
