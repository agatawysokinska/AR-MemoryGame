using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using Unity.Netcode.Components;
using Unity.VisualScripting;

public class CardRotation : NetworkBehaviour
{
    private Touch touch;
    public Animator cardAnimator;
    public NetworkAnimator cardAnimatorNet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            var clientId = OwnerClientId;
            Debug.Log("client ID: " + clientId);
            Debug.Log("Local client ID: " + NetworkManager.LocalClientId);

            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit;
            
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        Debug.Log("Card touched: " + gameObject.name);
                        // Call the server RPC to change ownership
                        GetComponent<CardOwnership>().ChangeOwnershipServerRpc(NetworkManager.LocalClientId, new ServerRpcParams());

                        cardAnimator.SetTrigger("FlipCard");
                    }
                }
            }
        }
    }
}