using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Obstacle : NetworkBehaviour
{
    public virtual void EffectOnSpeed(PlayerMovement car) { }
}
