using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboardCanvas;

    [Space]
    [SerializeField] private GameObject _boardCellTemplate;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private DatabaseInfo _databaseInfo;
    [SerializeField] private Button _openLeaderboardButton;
    [SerializeField] private Button _closeLeaderboardButton;

    [Inject] private DatabaseManager _databaseManager;

    private List<GameObject> _spawnedPlayers = new List<GameObject>();
    private readonly Color _userColorInLeaderboard = new Color(0.9f, 0.3f, 0.38f);
    

    private void Awake()
    {
        _openLeaderboardButton.onClick.AddListener(UpdatePlayerData);
        _closeLeaderboardButton.onClick.AddListener(CloseLeaderBoard);
    }

    private async void UpdatePlayerData()
    {
        _leaderboardCanvas.SetActive(true);
        Dictionary<string, PlayerData> sortedPlayers = await _databaseInfo.GetSortedScoresAsync();
        SpawnList(sortedPlayers);
    }

    private void SpawnList(Dictionary<string, PlayerData> sortedPlayers)
    {
        int place = 1;

        foreach(var item in sortedPlayers)
        {
            if (place <= Constants.PlayersInLeaderboardCount || _databaseManager.FirebaseUser.UserId == item.Key)
            {
                GameObject playerObject = Instantiate(_boardCellTemplate, _spawnPoint);
                _spawnedPlayers.Add(playerObject);

                TemplateData templateData = playerObject.GetComponent<TemplateData>();
                if (templateData != null)
                {
                    templateData.SetData(place, item.Value.Name, item.Value.Score);

                    if (_databaseManager.FirebaseUser.UserId == item.Key)
                    {
                        templateData.SetColor(_userColorInLeaderboard);
                    }

                }
            }
            place++;
        }
    }

    private void CloseLeaderBoard()
    {
        ClearSpawnedPlayers();
        _leaderboardCanvas.SetActive(false);

    }

    private void ClearSpawnedPlayers()
    {
        foreach (var playerObject in _spawnedPlayers)
        {
            Destroy(playerObject); 
        }

        _spawnedPlayers.Clear(); 
    }
}
