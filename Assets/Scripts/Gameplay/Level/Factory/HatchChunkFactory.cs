using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class HatchChunkFactory : ChunkFactory
{
    public HatchChunkFactory(GameObject obstacle)
    {
        _prefab = obstacle;
    }
}
