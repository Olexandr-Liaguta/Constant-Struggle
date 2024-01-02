using UnityEngine;

public class InventoryEquipmentSlotUI : MonoBehaviour
{
    public EquipmentSlotExact equipmentSlot;

    [HideInInspector] public InventorySlotUI inventorySlotUI;

    private void Awake()
    {
        inventorySlotUI = GetComponentInChildren<InventorySlotUI>();
    }

}
