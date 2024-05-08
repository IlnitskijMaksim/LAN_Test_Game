using Cinemachine;
using Inventory.Model;
using Inventory.UI;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

namespace Inventory
{
    public class InventoryController : NetworkBehaviour
    {
        public UIInventoryPage inventoryUI;

        public InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>(); 

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();
            inventoryUI = FindObjectOfType<UIInventoryPage>();
            PrepareUI();
            PrepareInventoryData();
            inventoryUI.gameObject.SetActive(false);
        }

        // ϳ�������� ����� ���������
        private void PrepareInventoryData()
        {
            inventoryData.Initialize(); // ������������ ����� ���������
            inventoryData.OnInventoryUpdated += UpdateInventoryUI; // ϳ������ �� ���� ��������� ���������
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;
                inventoryData.AddItem(item); // ��������� ���������� �������� �� ���������
            }
        }

        // ��������� ���������� ���������
        public void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ReselAllItems(); // �������� ��� �������� UI
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity); // ��������� ����� �������� UI
            }
        }

        // ϳ�������� UI ���������
        private void PrepareUI()
        {
            inventoryUI.InitializeInventoryUI(inventoryData.Size); // ������������ UI ���������
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest; // ϳ������ �� ���� ������ ����� ��������
            inventoryUI.OnSwapItems += HandleSwapItems; // ϳ������ �� ���� ����� ����������
            inventoryUI.OnStartDragging += HandleDragging; // ϳ������ �� ���� ������� ������������� ��������
            inventoryUI.OnItemActionRequested += HandleItemActionRequest; // ϳ������ �� ���� ������ ��������� 䳿 ��� ���������
        }

        // �������� ��䳿 ������ ��������� 䳿 ��� ���������
        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                inventoryUI.ShowItemAction(itemIndex);
                inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
            }
        }

        // ��������� ��������
        public void DropItem(int itemIndex, int quantity)
        {
            inventoryData.RemoveItem(itemIndex, quantity);
            inventoryUI.ResetSelection();
        }

        // ��������� 䳿 ��� ���������
        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject);
                if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                    inventoryUI.ResetSelection();
            }
        }

        // �������� ��䳿 ������� ������������� ��������
        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        // �������� ��䳿 ����� ����������
        private void HandleSwapItems(int itemIndex1, int itemIndex2)
        {
            inventoryData.SwapItems(itemIndex1, itemIndex2);
        }

        // �������� ��䳿 ������ ����� ��������
        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
        }

        // ��������� � ������� ����
        public void Update()
        {
            
            if (!isLocalPlayer) { return; }
            if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
            {
                // ��������, �� ��������� ��������� ����������
                if (inventoryUI.isActiveAndEnabled == false)
                {
                    inventoryUI.Show(); // ³���������� ���������� ���������
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                    }
                }
                else
                {
                    inventoryUI.Hide(); // ���������� ���������� ���������
                }
            }
        }
    }
}