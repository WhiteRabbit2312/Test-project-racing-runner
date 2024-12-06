using UnityEngine;
using Fusion;

public class CameraFollow : NetworkBehaviour
{
    [SerializeField] private GameObject _text;
    public override void Spawned()
    {

        if (HasInputAuthority)
        {
            gameObject.GetComponent<Camera>().enabled = true;
            _text.SetActive(true);
        }
    }
}
