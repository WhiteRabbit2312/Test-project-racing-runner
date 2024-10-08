using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitro : Obstacle
{
    private readonly float _speedBoost = 10f;
    public override void EffectOnSpeed(CarController car)
    {
        car.Speed += _speedBoost;
    }
}
