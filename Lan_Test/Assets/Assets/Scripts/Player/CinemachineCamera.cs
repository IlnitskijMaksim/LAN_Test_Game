using UnityEngine;
using Cinemachine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Посилання на віртуальну камеру Cinemachine

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        if (virtualCamera == null)
        {
            Debug.LogError("Virtual Camera not assigned to Player Setup.");
            return;
        }

        // Призначення локального гравця до графи Follow у віртуальній камері
        virtualCamera.Follow = transform;
    }
}
