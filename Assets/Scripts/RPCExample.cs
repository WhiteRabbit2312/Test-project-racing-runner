using UnityEngine;
using Fusion;

public class RPCExample : MonoBehaviour
{
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_ShowItemEffect(Vector3 itemPosition)
    {
        // Логіка створення візуального ефекту
        CreateItemEffect(itemPosition);
    }

    private void CreateItemEffect(Vector3 position)
    {
        Debug.Log("Item effect activated at position: " + position);
    }

    // Метод, який викликає RPC
    public void ActivateItemEffect()
    {
        Vector3 itemPosition = transform.position;
        RPC_ShowItemEffect(itemPosition);
    }
}


