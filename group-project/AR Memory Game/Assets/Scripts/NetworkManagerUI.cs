using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkManagerUI : MonoBehaviour
{
    [SerializeField] private Button client_button;
    [SerializeField] private Button host_button;
    

    private void Awake()
    {
        client_button.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });

        host_button.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            //relay.CreateRelay();
        });
    }
}
