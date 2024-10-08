using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class EmptyChunkFactory : ChunkFactory
{
    public EmptyChunkFactory(NetworkObject obstacle)
    {
        _prefab = obstacle;
    }
}
