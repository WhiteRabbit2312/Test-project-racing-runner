using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;
using Fusion.Sockets;
using System;

public class GameStarter : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _addButton;
    [SerializeField] private NetworkRunner _networkRunnerPrefab; 
    [HideInInspector] public NetworkRunner NetRunner;
    [Inject] private DatabaseManager _databaseManager;

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

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        //Debug.LogError("OnPlayerJoined");

        //if (!PlayerUserID.ContainsValue(_databaseManager.FirebaseUser.UserId))
        //{
        //PlayerUserID.Add(player, _databaseManager.FirebaseUser.UserId);



        //Debug.LogError("Count: " + PlayerUserID.Count);
        //runner.SetMasterClient(player);



        //Debug.LogError("SHOW PLAYERS: " + _databaseManager.FirebaseUser.UserId);

        //}

    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
     
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
       
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
       
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
       
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
        
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }
}
