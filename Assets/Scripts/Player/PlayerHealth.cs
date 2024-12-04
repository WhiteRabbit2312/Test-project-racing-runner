using System;
using System.Linq;
using Fusion;
using UnityEngine;
using Zenject;

public class PlayerHealth : NetworkBehaviour
{
    [SerializeField] private int _health = 3;
    private PlayerSpawner _playerSpawner;

    [Inject]
    private void Construct(PlayerSpawner playerSpawner)
    {
        _playerSpawner = playerSpawner;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (TryGetComponent(out Obstacle obstacle))
        {
            HandleCrash();
        }
    }

    private void HandleCrash()
    {
        DecreaseHealth();
        if (_health <= 0)
        {
            transform.GetComponentInChildren<Camera>().enabled = false;
            var enemy = _playerSpawner.Players.Where(a => a.Key != Runner.LocalPlayer).FirstOrDefault();
            
            
        }
    }
    
    private void DecreaseHealth()
    {
        _health--;
    }
}
