using UnityEngine;
using Cinemachine;
using Mirror;

public class PlayerSetup : NetworkBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // ��������� �� ��������� ������ Cinemachine

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        if (virtualCamera == null)
        {
            Debug.LogError("Virtual Camera not assigned to Player Setup.");
            return;
        }

        // ����������� ���������� ������ �� ����� Follow � ��������� �����
        virtualCamera.Follow = transform;
    }
}
