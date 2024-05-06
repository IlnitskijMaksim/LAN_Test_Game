using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CameraToPlayer : NetworkBehaviour
{
    private GameObject player;
    private bool playerSet = false;

    public void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        if (!isLocalPlayer || playerSet || player == null)
        {
            return;
        }

        Vector3 temp = transform.position;
        temp.x = player.transform.position.x;
        temp.y = player.transform.position.y;
        transform.position = temp;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        if (isLocalPlayer)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerSet = true;
            }
        }
    }
}
