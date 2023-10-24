using System.Threading.Tasks;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Services.Authentication;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using TMPro;

public class Relay : MonoBehaviour
{
    public static Relay Instance { get; private set; }
    
    private string joinCode;
    // Start is called before the first frame update
    private async void Start()
    {
        await UnityServices.InitializeAsync();
        // Relay requires user to sign in, this creates an account and allows user to sign in anonymously
        AuthenticationService.Instance.SignedIn += () => {
            Debug.Log("Signed in "+ AuthenticationService.Instance.PlayerId);
        };
       // await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
  
    private void Awake()
    {
        Instance = this;
    }

    public async Task<string> CreateRelay(){
        try{
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(2); // here we put max number of connections

            // Generate a join code
            joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log("Join Code: "+joinCode);
            
            RelayServerData relayServerData = new RelayServerData(allocation,"dtls");
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartHost();

            return joinCode;
        } catch(RelayServiceException e){
            Debug.Log(e);
            return null;
        }
    }


    public async void JoinRelay(string joinCode){
        try{
            Debug.Log("Joining Relay with "+ joinCode);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData relayServerData = new RelayServerData(joinAllocation,"dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartClient();
        } catch(RelayServiceException e){
            Debug.Log(e);
        }
    }
}
