using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpilledOil : Obstacle
{
    private readonly float _slowDownDuration = 0.08f;
    private readonly float _effectDuration = 5f;

    public override void EffectOnSpeed(PlayerMovement car)
    {
        if(car.Speed > 1)
            car.Speed -= _slowDownDuration;
    }

   

}
