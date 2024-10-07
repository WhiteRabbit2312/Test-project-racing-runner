using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class DesktopInput : IInput
{
    private readonly float _leftPos = -1;
    private readonly float _rightPos = 1;
    public float Move()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.LogWarning("Go left");
            return _leftPos;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.LogWarning("Go right");

            return _rightPos;
        }

        else
        {
            return 0f;
        }
    }

}
