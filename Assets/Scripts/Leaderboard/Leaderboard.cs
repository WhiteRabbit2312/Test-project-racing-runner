using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private GameObject _leaderboardCanvas;

    [Space]
    [SerializeField] private GameObject _boardCellTemplate;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private DatabaseInfo _databaseInfo;
    [SerializeField] private Button _openLeaderboardButton;
    [SerializeField] private Button _closeLeaderboardButton;

    private List<GameObject> _spawnedPlayers = new List<GameObject>();

    private void Awake()
    {
        _openLeaderboardButton.onClick.AddListener(UpdatePlayerData);
        _closeLeaderboardButton.onClick.AddListener(CloseLeaderBoard);
    }

    private async void UpdatePlayerData()
    {
        _leaderboardCanvas.SetActive(true);
        List<PlayerData> sortedPlayers = await _databaseInfo.GetSortedScoresAsync();
        SpawnList(sortedPlayers);
    }

    private void SpawnList(List<PlayerData> sortedPlayers)
    {
        int playersInLeaderboard;

        if (sortedPlayers.Count < Constants.PlayersInLeaderboardCount)
        {
            playersInLeaderboard = sortedPlayers.Count;
        }

        else
        {
            playersInLeaderboard = Constants.PlayersInLeaderboardCount;
        }

        int place = 1;

        for (int i = 0; i < playersInLeaderboard; i++)
        {
            GameObject playerObject = Instantiate(_boardCellTemplate, _spawnPoint);
            _spawnedPlayers.Add(playerObject);

            TemplateData templateData = playerObject.GetComponent<TemplateData>();
            if (templateData != null)
            {
                templateData.SetData(place, sortedPlayers[i].Name, sortedPlayers[i].Score);
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
