using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class NitroChunkFactory : ChunkFactory
{
    public NitroChunkFactory(GameObject obstacle)
    {
        _prefab = obstacle;
    }
}
