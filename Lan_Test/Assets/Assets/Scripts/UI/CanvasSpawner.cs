using UnityEngine;
using Mirror;
using Inventory;

public class SpawnCanvasForPlayer : NetworkBehaviour
{
    public GameObject canvasPrefab; // ссылка на префаб канваса

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // Проверяем, есть ли ссылка на префаб канваса
        if (canvasPrefab != null)
        {
            // Спавним префаб канваса
            GameObject canvas = Instantiate(canvasPrefab);
        }
        else
        {
            Debug.LogWarning("Canvas prefab reference is missing.");
        }
    }
}
