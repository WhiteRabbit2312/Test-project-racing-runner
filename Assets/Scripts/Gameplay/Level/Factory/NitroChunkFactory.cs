using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NitroChunkFactory : ChunkFactory
{
    public NitroChunkFactory(NetworkObject obstacle)
    {
        _prefab = obstacle;
    }
}
