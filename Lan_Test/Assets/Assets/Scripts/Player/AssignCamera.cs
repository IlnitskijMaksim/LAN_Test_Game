using UnityEngine;
using Cinemachine;
using Mirror;

public class AutoAssignCameraTarget : NetworkBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // ���������, ��� ������ - ��� ��������� �����
        if (isLocalPlayer)
        {
            // �������� ��������� Cinemachine Virtual Camera
            virtualCamera = GetComponent<CinemachineVirtualCamera>();

            // ���� ����������� ������ �������, �������� ��������� ������ ������ � �������� Follow
            if (virtualCamera != null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    // ��������� ������ � �������� Follow ������ �� ��������� �������
                    virtualCamera.Follow = player.transform;
                }
                else
                {
                    Debug.LogWarning("Player object not found. Make sure to tag your player object with 'Player'.");
                }
            }
            else
            {
                Debug.LogWarning("Cinemachine Virtual Camera component not found.");
            }
        }
    }
}
