using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;
using TMPro;
using System.Linq;

public class LevelGeneration : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDebug;
    [SerializeField] private GameObject[] _testGO;
    [SerializeField] private GameObject _brokenCarPrefab;
    [SerializeField] private GameObject _spilledOilCarPrefab;
    [SerializeField] private GameObject _hatchPrefab;
    [SerializeField] private GameObject _nitroPrefab;
    [SerializeField] private GameObject _emptyPrefab;
    [SerializeField] private GameObject _finishPrefab;

    [Space]

    [SerializeField] private int _levelLength;
    [Networked]
    public int Seed { get; set; }

    private List<ChunkFactory> _obstaclesList = new List<ChunkFactory>();
    private ChunkFactory _firstChunk;
    private ChunkFactory _lastChunk;
    private readonly float _step = 6f;
    private readonly float _startPosition = 0f;

    public override void Spawned()
    {
        _firstChunk = new EmptyChunkFactory(_emptyPrefab);
        _obstaclesList.Add(new EmptyChunkFactory(_emptyPrefab));
        _obstaclesList.Add(new BrokenCarChunkFactory(_brokenCarPrefab));
        _obstaclesList.Add(new HatchChunkFactory(_hatchPrefab));
        _obstaclesList.Add(new SpilledOilChunkFactory(_spilledOilCarPrefab));
        _obstaclesList.Add(new NitroChunkFactory(_nitroPrefab));
        _lastChunk = new FinishChunkFactory(_finishPrefab);

        if (Runner.LocalPlayer.PlayerId == Constants.FirstPlayerID)
        {
            Seed = SetSeed();
        }

        GenerateLevel();
    }

    private int SetSeed()
    {
        return Seed = UnityEngine.Random.Range(0, Constants.GenerationRange);
    }

    private void GenerateLevel()
    {

        System.Random random = new System.Random(Seed);

        _textDebug.text = Seed.ToString();

        _firstChunk.CreateChunk(_startPosition);

        for (int i = 1; i < _levelLength; i++)
        {
            int randomNumber = random.Next(0, _obstaclesList.Count);
            _obstaclesList[randomNumber].CreateChunk(_step * i);
        }
        _lastChunk.CreateChunk(_step * _levelLength);
    }
}
