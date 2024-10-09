using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fusion;

public class PreGamePlayersInfoPanel : NetworkBehaviour
{
    [SerializeField] private Image[] _image; 
    [SerializeField] private TextMeshProUGUI[] _nick1;
    [SerializeField] private AvatarSpriteSO _avatarSpriteSO;

    public void InitPlayer(int playerID, string nick, int id)
    {
        _image[playerID].sprite = _avatarSpriteSO.SpriteAvatar[id];
        _nick1[playerID].text = nick;
    }

}
