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
            Debug.LogWarning("GetInput");
            if (data.HorizontalDirection.x != 0)
            {
                Debug.LogWarning("Move");
            }
        }
        else
        {
            Debug.LogWarning("Get input is null");
        }

        float moveX = Input.GetAxis("Horizontal"); // Используем горизонтальную ось (A, D или стрелки)
        Vector3 moveDirection = new Vector3(moveX, 0, 0); // Направление по оси X
        Debug.LogWarning("moveDirection: " + moveDirection);
    }
}
