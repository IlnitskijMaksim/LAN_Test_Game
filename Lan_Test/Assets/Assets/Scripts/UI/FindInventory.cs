using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Inventory.UI;

public class FindInventory : NetworkBehaviour
{
    [SerializeField] private UIInventoryPage inventoryPage;
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
    }
}
