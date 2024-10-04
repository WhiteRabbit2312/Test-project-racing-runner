using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Linq;
using UnityEngine.SceneManagement;

public class SessionPlayerConnectionCheck : NetworkBehaviour
{
    [SerializeField] private GameObject _informPanel;

    private void OnEnable()
    {
        CheckPlayers(GameStarter.Instance.NetworkRunner);
    }

    public void CheckPlayers(NetworkRunner runner)
    {
        int count = runner.ActivePlayers.Count();

        if (count == Constants.PlayersInSessionCount)
        {
            Debug.LogWarning("���� 2 ������ � ������");
            ShowPlayerInformPanel();
        }
        else
        {
            Debug.LogWarning($"������� ���������� �������: {count}");
        }
    }
    
    private void ShowPlayerInformPanel()
    {
        GameStarter.Instance.NetworkRunner.Spawn(_informPanel);
    }
}
