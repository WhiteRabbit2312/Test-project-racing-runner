using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float _speed;

    public float Speed
    {
        get { return _speed; }
        set
        {
            _speed = value;
        }
    }

    [Space]

    [SerializeField] private float _leftPosX;
    [SerializeField] private float _rightPosX; 
    private float _centerPosX = 0f;

    private PlayerPos _playerPos = PlayerPos.Center;

    private readonly int _leftPosIdx = -1;
    private readonly int _rightPosIdx = 1;
    private readonly float _turnSpeed = 10f;

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out CarInput data))
        {
            Debug.LogError("GetInput is true");
            if (data.HorizontalDirection.x == _leftPosIdx)
            {
                Left();
            }

            else if (data.HorizontalDirection.x == _rightPosIdx)
            {
                Right();
            }

        }

        transform.Translate(Vector3.forward * _speed * Time.deltaTime);
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out SpilledOil spilledOil))
        {
            spilledOil.EffectOnSpeed(this);
        }

        else if (other.TryGetComponent(out BrokenCar brokenCar))
        {
            brokenCar.EffectOnSpeed(this);
        }

        else if (other.TryGetComponent(out Hatch hatch))
        {
            hatch.EffectOnSpeed(this);
        }
    }
}
