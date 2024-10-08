using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenCar : Obstacle
{
    private readonly float _slowDownDuration = 0.4f;
    public override void EffectOnSpeed(CarController car)
    {
        car.Speed *= _slowDownDuration;
    }

}
