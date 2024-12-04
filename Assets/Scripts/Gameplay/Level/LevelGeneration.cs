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
    
    /*
    private void GenerateLevel()
    {
        System.Random random = new System.Random(Seed);

        _textDebug.text = Seed.ToString();

        // Задаем веса: пустой чанк вес = 5, остальные = 1
        List<int> weights = new List<int>();
        for (int i = 0; i < _obstaclesList.Count; i++)
        {
            weights.Add(i == 0 ? 5 : 1); // Первый элемент — пустой чанк
        }

        // Генерация уровня
        for (int i = 0; i < _levelLength; i++)
        {
            int randomIndex = GetWeightedRandomIndex(weights, random);
            GameObject chunk = _obstaclesList[randomIndex].CreateChunk(_step * i);

            if (chunk.transform.GetChild(0) != null)
            {
                chunk.transform.GetChild(0).position = new Vector3(GenerateXPosition(), 1, chunk.transform.position.z);
                chunk.transform.SetParent(transform);
            }
        }
    }

// Метод для выбора индекса с учетом весов
    private int GetWeightedRandomIndex(List<int> weights, System.Random random)
    {
        int totalWeight = 0;
        foreach (int weight in weights)
        {
            totalWeight += weight;
        }

        int randomValue = random.Next(0, totalWeight);
        for (int i = 0; i < weights.Count; i++)
        {
            if (randomValue < weights[i])
            {
                return i;
            }
            randomValue -= weights[i];
        }

        return 0; 
    }*/

    private float GenerateXPosition()
    {
        System.Random random = new System.Random(Seed);
        float x;
        int randomNumber = random.Next(0, 3);
        switch (randomNumber)
        {
            case 0: x = -8; break;
            case 1: x = 0; break;
            default: x = 8; break;

        }
        return x;

    }
}
