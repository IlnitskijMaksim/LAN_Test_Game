using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;

public class JoinGame : MonoBehaviour
{
    public NetworkManager networkManager;
    public TMP_InputField inputFieldIP;
    public Button buttonJoin;


    public void Start()
    {
        if (networkManager == null)
        {
            networkManager = FindObjectOfType<NetworkManager>();
        }
        if (buttonJoin != null)
        {
            buttonJoin.onClick.AddListener(OnJoinButtonClicked);
        }
    }

    public void OnJoinButtonClicked()
    {
        string ipAddress = inputFieldIP.text;
        Debug.Log("IP Address entered: " + ipAddress); 
        if (!string.IsNullOrEmpty(ipAddress))
        {
            networkManager.networkAddress = ipAddress;
            networkManager.StartClient();
            Debug.Log("Attempting to join server at: " + ipAddress);
        }
        else
        {
            Debug.LogError("IP Address is empty!");
        }
    }
}
