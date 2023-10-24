using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CardOwnership : NetworkBehaviour
{
    // Function to change ownership
    [ServerRpc(RequireOwnership = false)]
    public void ChangeOwnershipServerRpc(ulong newOwnerId, ServerRpcParams serverRpcParams)
    {
        var currentOwnerId = GetComponent<NetworkObject>().OwnerClientId;
        if (newOwnerId == currentOwnerId)
        {
            // No need to change ownership if the new owner is the same as the current owner
            return;
        }

        var clientID = serverRpcParams.Receive.SenderClientId;
        Debug.Log("client ID who requested: " + clientID);
        GetComponent<NetworkObject>().ChangeOwnership(newOwnerId);
    }
}
