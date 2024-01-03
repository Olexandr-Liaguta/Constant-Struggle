using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Button itemButton;

    public Guid id = Guid.NewGuid();

    InventoryItem inventoryItem;

    public void SetItem(InventoryItem newItem)
    {
        inventoryItem = newItem;

        icon.sprite = inventoryItem.item.icon;
        icon.enabled = true;

        itemButton.interactable = true;
    }

    public void ClearSlot()
    {
        inventoryItem = null;

        icon.sprite = null;
        icon.enabled = false;

        itemButton.interactable = false;
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
        PlayerInventoryManager.Instance.SelectInventoryItem(id);
    }

    public void UseItem()
    {
        if (inventoryItem != null)
        {
            inventoryItem.Use();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inventoryItem != null)
        {
            TooltipManager.Instance.Show(inventoryItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.Hide();
    }
}
