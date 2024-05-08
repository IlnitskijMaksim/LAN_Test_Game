using UnityEngine;
using Mirror;
using Inventory;
using Inventory.UI;

public class SpawnCanvasForPlayer : NetworkBehaviour
{
    public GameObject canvasPrefab;
    [SerializeField] private UIInventoryPage inventoryPage;// ссылка на префаб канваса

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(canvasPrefab != null)
        {
            // Спавним префаб канваса
            GameObject canvas = Instantiate(canvasPrefab);
            inventoryPage = canvas.GetComponentInChildren<UIInventoryPage>();
        }
        else
        {
            Debug.LogWarning("Canvas prefab reference is missing.");
        }
    }
}
