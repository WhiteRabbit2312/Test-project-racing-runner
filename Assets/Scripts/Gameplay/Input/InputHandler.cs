using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Zenject;

public class InputHandler : NetworkBehaviour
{
    private IInput _input;
    [Inject] private GameStarter _gameStarter;
    public void Init(IInput input)
    {
        Debug.LogWarning("Init");
        _input = input;
    }
    public override void Spawned()
    {
        GameStarter.Instance.NetworkRunner.GetComponent<NetworkEvents>().OnInput.AddListener(OnInputProvide);
    }

    private void OnInputProvide(NetworkRunner runner, NetworkInput input)
    {
        Debug.LogWarning("OnInputProvide");
        var data = new CarInput();
        data.HorizontalDirection = new Vector3(_input.Move(), 0, 0);
        Debug.LogWarning("_input.Move(): " + _input.Move());
        input.Set(data);
    }
}
