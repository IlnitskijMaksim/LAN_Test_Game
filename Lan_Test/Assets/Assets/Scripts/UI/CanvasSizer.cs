using UnityEngine;

public class AssignRenderCamera : MonoBehaviour
{
    void Start()
    {
        // Получаем компонент канваса
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            // Находим главную камеру в сцене
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                // Назначаем главную камеру в качестве рендер-камеры для канваса
                canvas.renderMode = RenderMode.ScreenSpaceCamera;
                canvas.worldCamera = mainCamera;
            }
            else
            {
                Debug.LogWarning("Main camera not found in the scene.");
            }
        }
        else
        {
            Debug.LogWarning("Canvas component is missing.");
        }
    }
}
