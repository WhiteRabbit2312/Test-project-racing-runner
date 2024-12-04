using UnityEngine;
using Fusion;

public class CameraFollow : NetworkBehaviour
{
    public override void Spawned()
    {

        if (HasInputAuthority)
        {
            gameObject.GetComponent<Camera>().enabled = true;
        }
    }
}
