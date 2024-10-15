using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class AvatarPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerText;
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private Image _playerAvatar;
    [SerializeField] private AvatarSpriteSO _avatarSpriteSO;
    [SerializeField] private DatabaseInfo _databaseInfo;

    [Space]
    [SerializeField] private GameObject _canvasAvatar;
    [SerializeField] private Button _openAvatarButton;
    [SerializeField] private Button _closeButton;

    [Inject] private DatabaseManager _databaseManager;

    private void Start()
    {
        _openAvatarButton.onClick.AddListener(OpenPanel);
        _closeButton.onClick.AddListener(GetAvatar);
        _closeButton.onClick.AddListener(ClosePanel);
        _closeButton.onClick.AddListener(GetName);

        _nameInputField.onValueChanged.AddListener(SetName);

        GetAvatar();
        GetName();
    }

    public void SetAvatar(int id)
    {
        _databaseInfo.SetData(Constants.DatabaseAvatarKey, id);
    }

    private async void GetAvatar()
    {
        string value = await _databaseInfo.GetPlayerData(Constants.DatabaseAvatarKey, _databaseManager.FirebaseUser.UserId);

        int avatarId = int.Parse(value);

        _playerAvatar.sprite = _avatarSpriteSO.SpriteAvatar[avatarId];
    }

    private void SetName(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {

            _databaseInfo.SetData(Constants.DatabaseNameKey, name);

        }
    }

    private async void GetName()
    {
        string value = await _databaseInfo.GetPlayerData(Constants.DatabaseNameKey, _databaseManager.FirebaseUser.UserId);
        _playerText.text = value;
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
