using UnityEngine;
using TMPro;
using Fusion;

public class FinalWindow : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _bestTimeText;
    [SerializeField] private TextMeshProUGUI _placeText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private FinalPlayerData _finalPlayerData;

    private void OnEnable()
    {
        
    }

    private void ShowResults()
    {

    }
}
