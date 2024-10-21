using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Zenject; 

public class InputHandler : NetworkBehaviour
{
    private IInput _input;
    public void Init(IInput input)
    {
        _input = input;
    }
    public override void Spawned()
    {
        if (Runner.IsClient)
        {
            Runner.GetComponent<NetworkEvents>().OnInput.AddListener(OnInputProvide);
        }

    }

    public void OnInputProvide(NetworkRunner runner, NetworkInput input)
    {
        var data = new CarInput();
        data.HorizontalDirection = _input.Move();
        input.Set(data);

    }

    
}
