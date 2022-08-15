using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button itemButton;

    public Guid id = Guid.NewGuid();

    InventoryUI inventoryUI;

    Item item;

    private void Start()
    {
        inventoryUI = GetComponentInParent<InventoryUI>();
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        itemButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        itemButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void SetSelectedState(bool value)
    {
        if (value)
        {
            itemButton.Select();
        }
    }

    public void SelectItem()
    {
        inventoryUI.SelectInventoryItem(id);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }


}
