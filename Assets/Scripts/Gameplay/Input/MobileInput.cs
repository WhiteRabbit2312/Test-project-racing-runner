using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class MobileInput : IInput
{
    private readonly float _leftPos = -1;
    private readonly float _rightPos = 1;

    public float Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            return _leftPos;
        }

        if (Input.GetKey(KeyCode.D))
        {
            return _rightPos;
        }

        else
        {
            return 0f;
        }
    }
}
