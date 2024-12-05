using System.Linq;
using UnityEngine;
using TMPro;
using Fusion;
using Zenject;

public class FinalWindow : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _scoreText;
    [SerializeField] private GameObject _panel;
    [Inject] private DatabaseInfo _playerData;

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_ShowResults()
    {
        Debug.LogError("ShowResults");
        _panel.SetActive(true);
        int counter = 0;
        foreach (var item in PlayerSpawner.Instance.Players)
        {
            NetworkObject player = item.Value;
            int score = player.GetComponent<PlayerMovement>().Score;
            _scoreText[counter].text = score.ToString();
            counter++;
            
            _playerData.SetData(Constants.DatabaseScoreKey, score);
        }
        
    }
}
