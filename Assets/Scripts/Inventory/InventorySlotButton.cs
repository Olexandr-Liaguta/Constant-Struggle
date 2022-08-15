using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotButton : MonoBehaviour, IPointerClickHandler
{
    InventorySlot inventorySlot;

    private void Start()
    {
        inventorySlot = GetComponentInParent<InventorySlot>();
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
