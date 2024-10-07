using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

public class GameStarter : NetworkBehaviour
{
    [SerializeField] private NetworkRunner _networkRunnerPrefab; 
    [HideInInspector] public NetworkRunner NetworkRunner;

    [Inject] private DatabaseManager _databaseManager;

    [HideInInspector] public Dictionary<PlayerRef, string> PlayerUserID = new Dictionary<PlayerRef, string>();

    public static GameStarter Instance;

    public async void OnStartGameButton()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        if (NetworkRunner == null)
        {
            NetworkRunner = Instantiate(_networkRunnerPrefab);
            NetworkRunner.name = Constants.NetworkRunnerName;
            NetworkRunner.ProvideInput = true;

            var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
            var sceneInfo = new NetworkSceneInfo();
            if (scene.IsValid)
            {
                sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
            }


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
            SessionName = Constants.SessionName,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });

        if (result.Ok)
        {
            PlayerUserID.Add(runner.LocalPlayer, _databaseManager.FirebaseUser.UserId);
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
