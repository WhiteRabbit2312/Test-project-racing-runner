using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarPanel : MonoBehaviour
{
    [SerializeField] private Image _playerAvatar;
    [SerializeField] private AvatarSpriteSO _avatarSpriteSO;
    [SerializeField] private DatabaseInfo _databaseInfo;

    [Space]
    [SerializeField] private GameObject _canvasAvatar;
    [SerializeField] private Button _openAvatarButton;
    [SerializeField] private Button _closeButton;

    private void Awake()
    {
        _openAvatarButton.onClick.AddListener(OpenPanel);
        _closeButton.onClick.AddListener(GetAvatar);
        _closeButton.onClick.AddListener(ClosePanel);

        GetAvatar();
    }

    public void SetAvatar(int id)
    {
        _databaseInfo.SetData(Constants.DatabaseAvatarKey, id);
    }

    private async void GetAvatar()
    {
        int avatarId = await _databaseInfo.GetAvatarID();

        _playerAvatar.sprite = _avatarSpriteSO.SpriteAvatar[avatarId];
    }

    private void OpenPanel()
    {
        _canvasAvatar.SetActive(true);
    }

    private void ClosePanel()
    {
        _canvasAvatar.SetActive(false);
    }
}
