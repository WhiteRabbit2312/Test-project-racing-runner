using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : Obstacle
{
    public override void EffectOnSpeed(CarController car) 
    {
        float zeroSpeed = 0;
        car.Speed = zeroSpeed;
    }


}
