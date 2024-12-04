using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitro : Obstacle
{
    private readonly float _speedBoost = 0.001f;
    public override void EffectOnSpeed(PlayerMovement car)
    {
        car.Speed += _speedBoost;
    }
}
