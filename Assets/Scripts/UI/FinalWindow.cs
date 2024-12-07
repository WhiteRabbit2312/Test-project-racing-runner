using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Fusion;
using Zenject;

public class FinalWindow : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _scoreText;  
    [SerializeField] private TextMeshProUGUI[] _nameText;  
    [SerializeField] private GameObject _panel;

    private Dictionary<string, int> _playersScore = new Dictionary<string, int>();
    
    [Inject] private DatabaseInfo _playerData;

    [Networked] private int Score { get; set; }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_ShowResults()
    {
        Debug.LogError("ShowResults. COunt: " + _playersScore.Count);
        _panel.SetActive(true);
        int counter = 0;
        
        //_playerData.SetData(Constants.DatabaseScoreKey, PlayerSpawner.Instance.Players[Runner.LocalPlayer].GetComponent<PlayerMovement>().Score);
        string name = PlayerPrefs.GetString(Constants.DatabaseNameKey);
        _playerData.SetData(Constants.DatabaseScoreKey, _playersScore[name]);
        foreach (var player in _playersScore)
        {
            _scoreText[counter].text = player.Value.ToString();
            _nameText[counter].text = player.Key.ToString();
            counter++;
        }

        return;
        foreach (var item in PlayerSpawner.Instance.Players)
        {
            NetworkObject player = item.Value;
            Score = player.GetComponent<PlayerMovement>().Score;
            _scoreText[counter].text = Score.ToString();
            counter++;
            
            _playerData.SetData(Constants.DatabaseScoreKey, Score);
        }
        
    }

    public void AddPlayerData(string playerName, int score)
    {
        Debug.LogError("Add PlayerData: " + playerName + ", Score: " + score);
        RPC_AddScore(playerName, score);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All, InvokeLocal = true)]
    private void RPC_AddScore(string playerName, int score)
    {
        _playersScore.Add(playerName, score);
    }
}
