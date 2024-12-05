using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Fusion;
using Zenject;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private MainWindow _mainWindow;
    
    [SerializeField] private int _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _leftPosX;
    [SerializeField] private float _rightPosX;

    [SerializeField] private Vector3 _boxSize;
    
    public static Action OnPlayerDeath;
    
    private float _score;
    private readonly int _scoreAmount = 1;
    private readonly int _framePerSecond = 200;
    
    
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
        }
    }

    public float Speed
    {
        get { return _speed; }
        set
        {
            _speed = value;
        }
    }

    public int Score
    {
        get { return (int)_score; }
    }

    public bool IsAlive
    {
        get { return Health > 0; }
    }
    
    private float _centerPosX = 0f;

    private PlayerPos _playerPos = PlayerPos.Center;

    private readonly int _leftPosIdx = -1;
    private readonly int _rightPosIdx = 1;
    private readonly float _turnSpeed = 10f;

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out CarInput data))
        {
            if (data.HorizontalDirection.x == _leftPosIdx)
            {
                Left();
            }

            else if (data.HorizontalDirection.x == _rightPosIdx)
            {
                Right();
            }

        }

        transform.Translate(Vector3.forward * _speed);
        CountScore();
        DetectObstacle();
    }

    private void CountScore()
    {
        _score += (_scoreAmount * _speed) / _framePerSecond;
        _mainWindow.ShowScore((int)_score);
    }
    
    private void Left()
    {
        StopCoroutine(ChangePosition());

        if (_playerPos == PlayerPos.Center)
        {
            StartCoroutine(ChangePosition(_leftPosX));

            _playerPos = PlayerPos.Left;
            Debug.LogError("_leftPosX");

        }

        else if (_playerPos == PlayerPos.Right)
        {
            StartCoroutine(ChangePosition(_centerPosX));
            _playerPos = PlayerPos.Center;
            Debug.LogError("_centerPosX");

        }
    }
    private void Right()
    {
        StopCoroutine(ChangePosition());
        if (_playerPos == PlayerPos.Center)
        {
            StartCoroutine(ChangePosition(_rightPosX));
            Debug.LogError("_rightPosX");
            _playerPos = PlayerPos.Right;
        }

        else if (_playerPos == PlayerPos.Left)
        {
            StartCoroutine(ChangePosition(_centerPosX));
            Debug.LogError("_centerPosX");

            _playerPos = PlayerPos.Center;
        }

    }

    private IEnumerator ChangePosition(float newX = 0)
    {
        float t = 0f;
        Vector3 startPos = transform.position;
        while (transform.position.x != newX)
        {
            t += Time.deltaTime * _turnSpeed;
            transform.position = Vector3.Lerp(startPos,
                new Vector3(newX, transform.position.y, transform.position.z), t);
            yield return new WaitForEndOfFrame();
        }

    }
    
    private void DetectObstacle()
    {
        Vector3 boxCenter = transform.position;
        
        Collider[] hitColliders = Physics.OverlapBox(boxCenter, _boxSize / 2, Quaternion.identity);
        
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent(out Obstacle obstacle))
            {
                obstacle.EffectOnSpeed(this);
                _mainWindow.ShowHealth(Health);
                //Destroy(obstacle.gameObject);
                Debug.LogError("Obstacle detected");
                HandleCrash();
            }
        }
    }
    
    private void HandleCrash()
    {
        if (Health <= 0) return;
        var enemy = PlayerSpawner.Instance.Players.FirstOrDefault(a => a.Key != Runner.LocalPlayer).Value;
        if(enemy == null) return;
        DecreaseHealth();
        if (Health <= 0)
        {
            transform.GetComponentInChildren<Camera>().enabled = false;
            Debug.LogError("Players Count (PlayerMovement): " + PlayerSpawner.Instance.Players.Count);
            if(enemy.GetComponentInChildren<Camera>() == null)Debug.LogError("camera not found");
            enemy.GetComponentInChildren<Camera>().enabled = true;
            //Runner.Despawn(GetComponent<NetworkObject>());
            OnPlayerDeath?.Invoke();
        }
    }
    
    private void DecreaseHealth()
    {
        Health--;
    }

    private void SetScore()
    {
        
    }
}
