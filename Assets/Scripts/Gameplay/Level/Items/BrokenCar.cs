using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenCar : Obstacle
{
    private readonly float _slowDownDuration = 0.4f;
    public override void EffectOnSpeed(PlayerMovement car)
    {
        //Debug.LogError("BrokenCar");
        car.Speed *= _slowDownDuration;
    }

}
