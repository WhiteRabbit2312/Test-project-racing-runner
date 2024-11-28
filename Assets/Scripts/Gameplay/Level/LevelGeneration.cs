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
    [SerializeField] private GameObject[] _chunkPrefab;

    [Space]

    [SerializeField] private int _levelLength;
    [Networked]
    public int Seed { get; set; }

    private List<ChunkFactory> _obstaclesList = new List<ChunkFactory>();
    private readonly float _step = 6f;
    private readonly float _startPosition = 0f;
    [Networked] private int PositionIdx { get; set; }
    public override void Spawned()
    {
        foreach (var chunk in _chunkPrefab)
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

            if(chunk.transform.GetChild(0) != null)
            {
                chunk.transform.GetChild(0).position = new Vector3(GenerateXPosition(), 1, chunk.transform.position.z);
            }
        }
    }

    private float GenerateXPosition()
    {
        float x;
        PositionIdx = UnityEngine.Random.Range(0, 3);
        switch (PositionIdx)
        {
            case 0: x = -1; break;
            case 1: x = 0; break;
            default: x = 1; break;

        }
        return x;

    }
}
