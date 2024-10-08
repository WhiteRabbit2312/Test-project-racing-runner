using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class BrokenCarChunkFactory : ChunkFactory
{
    public BrokenCarChunkFactory(NetworkObject obstacle)
    {
        _prefab = obstacle;
    }
}
