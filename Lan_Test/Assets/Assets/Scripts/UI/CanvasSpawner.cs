using UnityEngine;
using Mirror;
using Inventory;

public class SpawnCanvasForPlayer : NetworkBehaviour
{
    public GameObject canvasPrefab; // ������ �� ������ �������

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // ���������, ���� �� ������ �� ������ �������
        if (canvasPrefab != null)
        {
            // ������� ������ �������
            GameObject canvas = Instantiate(canvasPrefab);
        }
        else
        {
            Debug.LogWarning("Canvas prefab reference is missing.");
        }
    }
}
