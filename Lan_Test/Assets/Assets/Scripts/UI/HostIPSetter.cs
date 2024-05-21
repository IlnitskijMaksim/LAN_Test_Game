using System.Net;
using System.Net.NetworkInformation;
using System.Linq;
using UnityEngine;
using Mirror;

public class HostIPSetter : MonoBehaviour
{
    public NetworkManager networkManager;
    public string vpnAdapterName = "Radmin VPN";

    void Start()
    {
        if (networkManager == null)
        {
            networkManager = FindObjectOfType<NetworkManager>();
        }
    }

    public void SetHostIP()
    {
        string vpnIP = GetVPNIPAddress(vpnAdapterName);
        if (!string.IsNullOrEmpty(vpnIP))
        {
            networkManager.networkAddress = vpnIP;
            Debug.Log("Network Address set to: " + vpnIP);
        }
        else
        {
            Debug.LogError("Failed to get VPN IP address.");
        }
    }

    private string GetVPNIPAddress(string adapterName)
    {
        try
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.Name == adapterName)
                {
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            return ip.Address.ToString();
                        }
                    }
                }
            }
            throw new System.Exception($"No network adapters with name {adapterName} and IPv4 address found in the system!");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Exception in GetVPNIPAddress: " + ex.Message);
            return null;
        }
    }
}
