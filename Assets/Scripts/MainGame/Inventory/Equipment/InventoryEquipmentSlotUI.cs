using UnityEngine;

public class InventoryEquipmentSlotUI : MonoBehaviour
{
    public EquipmentSlotExact equipmentSlot;

    [HideInInspector]
    public InventorySlotUI slot;

    private void Start()
    {
        slot = GetComponentInChildren<InventorySlotUI>();
    }

}
