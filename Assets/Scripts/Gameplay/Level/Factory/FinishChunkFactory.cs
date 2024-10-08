using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class FinishChunkFactory : ChunkFactory
{
    public FinishChunkFactory(NetworkObject obstacle)
    {
        _prefab = obstacle;
    }
}
