using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : Obstacle
{
    public override void EffectOnSpeed(PlayerMovement car) 
    {
        Debug.LogError("Hatch");

        float zeroSpeed = 0;
        car.Speed = zeroSpeed;
        
    }


}
