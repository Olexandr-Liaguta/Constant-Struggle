using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotButton : MonoBehaviour, IPointerClickHandler
{
    InventorySlotUI inventorySlot;

    private void Start()
    {
        inventorySlot = GetComponentInParent<InventorySlotUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            inventorySlot.SelectItem();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            inventorySlot.UseItem();
        }
    }
}
