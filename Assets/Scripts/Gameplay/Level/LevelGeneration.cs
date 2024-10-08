using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System;

public class LevelGeneration : NetworkBehaviour
{
    [Networked] private int Seed { get; set; }
    [SerializeField] private int _levelLength;
    [SerializeField] private ObstaclePrefabSO _obstaclePrefabSO;
    private readonly float _step = 6f;

    private void Awake()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        Seed = Environment.TickCount;
        System.Random random = new System.Random(Seed);

        for (int i = 0; i < _levelLength; i++)
        {
            int randomNumber = random.Next(0, 6);
            _obstaclePrefabSO.ObstaclePrefab[randomNumber].CreateChunk(_step * i);
            Console.WriteLine(randomNumber);
        }
    }
}
