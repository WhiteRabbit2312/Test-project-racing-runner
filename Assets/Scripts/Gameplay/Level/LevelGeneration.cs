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
    [SerializeField] private GameObject[] _obstacleChunks;

    [Space]

    [SerializeField] private int _levelLength;
    [Networked]
    public int Seed { get; set; }
    [Networked] 
    private int _obstaclePosition { get; set; }

    private List<ChunkFactory> _obstaclesList = new List<ChunkFactory>();
    private ChunkFactory _firstChunk;
    private ChunkFactory _lastChunk;
    private readonly float _step = 6f;
    private readonly float _startPosition = 0f;

    public override void Spawned()
    {
        foreach (var chunk in _obstacleChunks) 
        {
            _obstaclesList.Add(new ChunkFactory(chunk));
        }

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

        for (int i = 0; i < _levelLength; i++)
        {
            int randomNumber = random.Next(0, _obstaclesList.Count);
            GameObject chunk = _obstaclesList[randomNumber].CreateChunk(_step * i);

            _obstaclePosition = UnityEngine.Random.Range(0, Constants.GenerationRange);
            float x = SetObstaclePosition(_obstaclePosition);
            Vector3 gameObject = chunk.transform.GetChild(0).position;
            chunk.transform.GetChild(0).transform.position = new Vector3(gameObject.x + x, gameObject.y, gameObject.z);
        }
    }

    public float SetObstaclePosition(int obstaclePosition)
    {
        float x;
        switch (obstaclePosition)
        {
            case 0: x = -1; break;
            case 1: x = 0; break;
            default: x = 1; break;
        }
        return x;
    }
}
