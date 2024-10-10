using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fusion;

public class PreGamePlayersInfoPanel : NetworkBehaviour
{
    [SerializeField] private Image[] _image; 
    [SerializeField] private TextMeshProUGUI[] _nick;
    [SerializeField] private AvatarSpriteSO _avatarSpriteSO;

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_InitPlayer(int playerID, string nick, string avatarID)
    {
        if (int.TryParse(avatarID, out int id))
        {
            _image[playerID].sprite = _avatarSpriteSO.SpriteAvatar[id];
            _nick[playerID].text = nick;

            Debug.LogError("nick: " + nick + "avatarID: " + avatarID);
        }

        else
        {
            Debug.LogError("Did not try parse");
        }
    }

}
