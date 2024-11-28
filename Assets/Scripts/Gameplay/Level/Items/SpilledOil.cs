using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpilledOil : Obstacle
{
    private readonly float _slowDownDuration = 1f;
    private readonly float _effectDuration = 5f;

    public override void EffectOnSpeed(PlayerMovement car)
    {
        car.Speed -= _slowDownDuration;
    }

   

}
