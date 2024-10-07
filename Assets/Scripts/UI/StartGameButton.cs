using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StartGameButton : MonoBehaviour
{
    [Inject] private GameStarter _gameStarter;
    private void Awake()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(_gameStarter.OnStartGameButton);
    }
}
