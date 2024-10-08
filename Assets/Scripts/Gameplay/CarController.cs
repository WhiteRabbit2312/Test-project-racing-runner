using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CarController : NetworkBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    public float Speed
    {
        get { return _speed; }
        set
        {
            _speed = value;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out SpilledOil spilledOil))
        {
            spilledOil.EffectOnSpeed(this);
        }

        else if(other.TryGetComponent(out BrokenCar brokenCar))
        {
            brokenCar.EffectOnSpeed(this);
        }
        
        else if (other.TryGetComponent(out Hatch hatch))
        {
            hatch.EffectOnSpeed(this);
        }
    }
}
