using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;


public class GameStarter : MonoBehaviour
{
    [SerializeField] private Button _startButton;

    [SerializeField] private NetworkRunner _networkRunnerPrefab; 
    [HideInInspector] public NetworkRunner NetworkRunner;



    public static GameStarter Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        _startButton.onClick.AddListener(OnStartGameButton);
    }

    public async void OnStartGameButton()
    {
        if (NetworkRunner == null)
        {
            NetworkRunner = Instantiate(_networkRunnerPrefab);
            NetworkRunner.name = "NetworkRunner";
            await StartGame(NetworkRunner);
        }
        else
        {
            Debug.LogWarning("Network runner is null");
        }
    }

    private async Task StartGame(NetworkRunner runner)
    {
        var result = await runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = "GameSession",
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        }) ;

        if (result.Ok)
        {
            LoadPreGameScene();
            Debug.LogWarning("Успешно подключились к сессии!");
        }
        else
        {
            Debug.LogError($"Ошибка подключения: {result.ShutdownReason}");
        }
    }

    private void LoadPreGameScene()
    {
        SceneManager.LoadScene(Constants.PreGameplaySceneIdx);
    }
    
}
