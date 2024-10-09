using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStrategy : MonoBehaviour
{
    private void Awake()
    {
//#if UNITY_EDITOR
        IInput input = new DesktopInput();
        /*
#else
        Input input = new MobileInput();
#endif*/
        InputHandler movementHandler = GetComponent<InputHandler>();
        movementHandler.Init(input);
    }
}
