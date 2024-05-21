using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public HostIPSetter hostIPSetter;
    public NetworkManager networkManager;

    void Start()
    {
        if (hostIPSetter == null)
        {
            hostIPSetter = FindObjectOfType<HostIPSetter>();
        }
    }

    public void OnHostButtonClicked()
    {
        hostIPSetter.SetHostIP();
        networkManager.StartHost();
    }
}

