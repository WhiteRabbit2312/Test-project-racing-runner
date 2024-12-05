using System.Linq;
using UnityEngine;
using TMPro;
using Fusion;
using Zenject;

public class FinalWindow : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _bestTimeText;
    [SerializeField] private TextMeshProUGUI _placeText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private FinalPlayerData _finalPlayerData;

    [Inject] private PlayerData _playerData;

    [Rpc(RpcSources.All, RpcTargets.All)]
    private void RPC_ShowResults()
    {
        
        //int score = PlayerSpawner.Instance.Players.FirstOrDefault(a => a.Key != Runner.LocalPlayer).Value
    }
}
