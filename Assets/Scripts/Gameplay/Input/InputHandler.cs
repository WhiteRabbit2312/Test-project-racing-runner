using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Zenject; 

public class InputHandler : NetworkBehaviour, IBeforeUpdate
{
    private IInput _input;
    [Inject] private GameStarter _gameStarter;
    private NetworkInputData _inputData;
    public void Init(IInput input)
    {
        Debug.LogWarning("Init");
        _input = input;
    }
    public override void Spawned()
    {
        //GameStarter.Instance.NetworkRunner.GetComponent<NetworkEvents>().OnInput.AddListener(OnInputProvide);


        var networkEvents = Runner.GetComponent<NetworkEvents>();
        networkEvents.OnInput.AddListener(OnInputProvide);

        Cursor.lockState = CursorLockMode.Locked;
    }

    void IBeforeUpdate.BeforeUpdate()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _inputData.Direction = horizontal * transform.right + vertical * transform.forward;


    }

    private void OnInputProvide(NetworkRunner runner, NetworkInput input)
    {
        /*
        Debug.LogWarning("OnInputProvide");
        var data = new CarInput();
        data.HorizontalDirection = new Vector3(_input.Move(), 0, 0);
        Debug.LogWarning("_input.Move(): " + _input.Move());
        input.Set(data);*/

        input.Set(_inputData);
    }

    
}

public struct NetworkInputData : INetworkInput
{
    public Vector3 Direction;
    public float MouseXRotation;
    public float MouseYRotation;
    public NetworkButtons Buttons;
    public bool isEPressed;
}
