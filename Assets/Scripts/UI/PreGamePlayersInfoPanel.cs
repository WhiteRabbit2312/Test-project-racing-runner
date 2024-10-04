using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Fusion;

public class PreGamePlayersInfoPanel : NetworkBehaviour
{
    [SerializeField] private Image _image1; 
    [SerializeField] private Image _image2;
    [SerializeField] private TextMeshProUGUI _nick1;
    [SerializeField] private TextMeshProUGUI _nick2;
    [SerializeField] private AvatarSpriteSO _avatarSpriteSO;

    private void Awake()
    {
        
    }

    public void InitPlayer(string nick, int id)
    {
        _image1.sprite = _avatarSpriteSO.SpriteAvatar[id];
        _nick1.text = nick;
    }

}
