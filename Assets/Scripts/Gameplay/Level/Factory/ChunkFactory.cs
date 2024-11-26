using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Zenject;

public class ChunkFactory : NetworkBehaviour
{
    protected GameObject _prefab;

    public ChunkFactory(GameObject prefab)
    {
        _prefab = prefab;
    }

    public GameObject CreateChunk(float obstaclePositionZ)
    {
        Vector3 _obstaclePosition = new Vector3(0, 0, obstaclePositionZ);
        GameObject prefabToSpawn = Instantiate(_prefab, _obstaclePosition, Quaternion.identity);
        return prefabToSpawn;
    }
}
