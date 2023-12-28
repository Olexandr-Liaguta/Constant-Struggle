using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public Button itemButton;

    public Guid id = Guid.NewGuid();

    InventoryUI inventoryUI;

    InventoryItem inventoryItem;

    private void Start()
    {
        inventoryUI = GetComponentInParent<InventoryUI>();
    }

    public void AddItem(InventoryItem newItem)
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
        inventoryUI.SelectInventoryItem(id);
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
            Tooltip.instance.ShowTooltip(inventoryItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.instance.HideTooltip();
    }
}
