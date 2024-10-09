using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Zenject;

public class ChunkFactory : NetworkBehaviour
{
    protected NetworkObject _prefab;
    public NetworkObject CreateChunk(float obstaclePositionZ)
    {
        Vector3 _obstaclePosition = new Vector3(0, 0, obstaclePositionZ);

        if (GameStarter.Instance.NetworkRunner != null)
        {
            NetworkObject prefabToSpawn = GameStarter.Instance.NetworkRunner.Spawn(_prefab, _obstaclePosition);
            return prefabToSpawn;
        }

        else
        {
            Debug.LogWarning("Runner is null");
            return null;
        }

    }
}
