using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

[CreateAssetMenu(menuName = "Obstacle", fileName = "Obstacle")]
public class ObstaclePrefabSO : ScriptableObject
{
    public ChunkFactory[] ObstaclePrefab;
}
