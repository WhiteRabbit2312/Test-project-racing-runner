using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : Obstacle
{
    public override void EffectOnSpeed(PlayerMovement car) 
    {
        car.Health++;
    }
}
