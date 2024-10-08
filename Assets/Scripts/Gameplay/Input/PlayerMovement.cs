using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerMovement : NetworkBehaviour
{
    public override void FixedUpdateNetwork()
    {
        Debug.LogWarning("Fixed update");

        if(GetInput(out CarInput data))
        {
            if (data.HorizontalDirection.x != 0)
            {
                transform.position = data.HorizontalDirection;
            }
        }
        else
        {
            Debug.LogWarning("Get input is null");
        }
    }
}
