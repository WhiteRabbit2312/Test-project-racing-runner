using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using UnityEngine.SceneManagement;


public class GameStarter : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _addButton;
    [SerializeField] private NetworkRunner _networkRunnerPrefab; 
    [HideInInspector] public NetworkRunner NetRunner;

    public static GameStarter Instance;
  
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _startButton.onClick.AddListener(OnStartGameButtonPressed);
        _addButton.onClick.AddListener(OnAddPlayerButtonPressed);
    }

    public void OnStartGameButtonPressed()
    {
        this.MyStartGame(Fusion.GameMode.Shared, Constants.SessionName);
    }

    public void OnAddPlayerButtonPressed()
    {
        this.MyStartGame(Fusion.GameMode.Client, Constants.SessionName);
    }

    public async void MyStartGame(GameMode mode, string sessionName)
    {

        if (NetRunner != null)
            return;
        NetRunner ??= gameObject.AddComponent<NetworkRunner>();
        
        NetRunner.ProvideInput = true;

        gameObject.AddComponent<NetworkEvents>();

        var scene = SceneRef.FromIndex(Constants.PreGameplaySceneIdx);
        var sceneInfo = new NetworkSceneInfo();

        if (scene.IsValid)
        {
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        }

        var result = await NetRunner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            CustomLobbyName = Constants.LobbyName,
            SessionName = sessionName,
            Scene = scene,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });

        if (!result.Ok)
        {
            Debug.LogError("result is not ok");
            
        }

    }

}
