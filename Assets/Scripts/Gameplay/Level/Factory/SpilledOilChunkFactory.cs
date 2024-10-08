using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class SpilledOilChunkFactory : ChunkFactory
{
    public SpilledOilChunkFactory(NetworkObject obstacle)
    {
        _prefab = obstacle;
    }
}
