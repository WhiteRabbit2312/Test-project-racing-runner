using UnityEngine;
using Fusion;

public class CameraFollow : NetworkBehaviour
{
    public override void Spawned()
    {
        gameObject.SetActive(HasInputAuthority);
    }
}
