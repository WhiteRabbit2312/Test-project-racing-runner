using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public class LevelGeneration : NetworkBehaviour
{
    [SerializeField] private NetworkObject _brokenCarPrefab;
    [SerializeField] private NetworkObject _spilledOilCarPrefab;
    [SerializeField] private NetworkObject _hatchPrefab;
    [SerializeField] private NetworkObject _nitroPrefab;
    [SerializeField] private NetworkObject _emptyPrefab;
    [SerializeField] private NetworkObject _finishPrefab;

    [Space]

    [SerializeField] private int _levelLength;
    [Networked] private int Seed { get; set; }
    
    private List<ChunkFactory> _obstaclesList = new List<ChunkFactory>();
    private ChunkFactory _lastChunk;
    private readonly float _step = 6f;

    public override void Spawned()
    {
        
        _obstaclesList.Add(new EmptyChunkFactory(_emptyPrefab));
        _obstaclesList.Add(new BrokenCarChunkFactory(_brokenCarPrefab));
        _obstaclesList.Add(new HatchChunkFactory(_hatchPrefab));
        _obstaclesList.Add(new SpilledOilChunkFactory(_spilledOilCarPrefab));
        _obstaclesList.Add(new NitroChunkFactory(_nitroPrefab));
        _lastChunk = new FinishChunkFactory(_finishPrefab);

        GenerateLevel();
    }

    private void GenerateLevel()
    {
        Seed = Environment.TickCount;
        System.Random random = new System.Random(Seed);

        for (int i = 0; i < _levelLength; i++)
        {
            int randomNumber = random.Next(0, 6);
            _obstaclesList[randomNumber].CreateChunk(_step * i);
            Console.WriteLine(randomNumber);
        }
        _lastChunk.CreateChunk(_step * _levelLength);
    }
}
