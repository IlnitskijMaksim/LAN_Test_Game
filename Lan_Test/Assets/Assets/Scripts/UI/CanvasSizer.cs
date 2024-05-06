using UnityEngine;

public class AssignRenderCamera : MonoBehaviour
{
    void Start()
    {
        // �������� ��������� �������
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            // ������� ������� ������ � �����
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                // ��������� ������� ������ � �������� ������-������ ��� �������
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
