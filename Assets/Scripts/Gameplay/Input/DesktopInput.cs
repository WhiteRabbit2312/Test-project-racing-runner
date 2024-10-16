using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class DesktopInput : IInput
{
    private readonly Vector3 _leftPos = new Vector3(-1, 1, 0);
    private readonly Vector3 _rightPos = new Vector3(1, 1, 0);
    public Vector3 Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.LogError("left");
            return _leftPos;
        }

        if (Input.GetKey(KeyCode.D))
        {
            Debug.LogError("right");

            return _rightPos;
        }

        else
        {
            return new Vector3(0, 0, 0);
        }
    }

}
