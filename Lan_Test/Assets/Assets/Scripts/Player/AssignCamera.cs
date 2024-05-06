using UnityEngine;
using Cinemachine;
using Mirror;

public class AutoAssignCameraTarget : NetworkBehaviour
{
    private CinemachineVirtualCamera virtualCamera;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // Проверяем, что объект - это локальный игрок
        if (isLocalPlayer)
        {
            // Получаем компонент Cinemachine Virtual Camera
            virtualCamera = GetComponent<CinemachineVirtualCamera>();

            // Если виртуальная камера найдена, пытаемся назначить объект игрока в параметр Follow
            if (virtualCamera != null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    // Назначаем игрока в параметр Follow только на локальном клиенте
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
