using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Zenject;

public class ChunkFactory : NetworkBehaviour
{
    [Inject] private GameStarter _gameStarter;
    protected NetworkObject _prefab;
    public NetworkObject CreateChunk(float obstaclePositionZ)
    {
        Vector3 _obstaclePosition = new Vector3(0, 0, obstaclePositionZ);
        if (_gameStarter.NetworkRunner != null)
        {
            NetworkObject prefabToSpawn = _gameStarter.NetworkRunner.Spawn(_prefab, _obstaclePosition);
            return prefabToSpawn;
        }

        else
        {
            Debug.LogWarning("Runner is null");
            return null;
        }

    }
}
