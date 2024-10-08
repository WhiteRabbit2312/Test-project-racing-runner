using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpilledOil : Obstacle
{
    private readonly float _slowDownDuration = 0.4f;
    private readonly float _effectDuration = 5f;

    public override void EffectOnSpeed(CarController car)
    {
        float tempSpeed = car.Speed;
        car.Speed *= _slowDownDuration;
        StartCoroutine(SlowDownCor(car, tempSpeed));
    }

    private IEnumerator SlowDownCor(CarController car, float speedBeforeEffect)
    {
        yield return new WaitForSeconds(_effectDuration);
        car.Speed = speedBeforeEffect;
    }

}
