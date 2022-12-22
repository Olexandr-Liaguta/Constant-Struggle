using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEquipmentUI : MonoBehaviour
{
    public Transform equipmentsParent;

    EquipmentManager equipmentManager;
    Dictionary<EquipmentSlotExact, InventoryEquipmentSlotUI> equipmentSlots = new();


    void Start()
    {
        equipmentManager = EquipmentManager.instance;
        equipmentManager.onEquipmentChanged += UpdateEquipmentUI;


        var equipmentSlotsInChildrens = equipmentsParent.GetComponentsInChildren<InventoryEquipmentSlotUI>();

        foreach (var equipmentSlot in equipmentSlotsInChildrens)
        {
            equipmentSlots.Add(equipmentSlot.equipmentSlot, equipmentSlot);
        }
    }


    void UpdateEquipmentUI(EquipmentSlotExact slot, InventoryItem newInventoryItem, InventoryItem oldInventoryItem)
    {
        if (newInventoryItem == null)
        {
            equipmentSlots.TryGetValue(slot, out InventoryEquipmentSlotUI equipment);

            if (equipment == null || equipment.slot == null)
            {
                return;
            }

            equipment.slot.ClearSlot();
        }
        else
        {
            Equipment newItem = newInventoryItem.item as Equipment;

            equipmentSlots.TryGetValue(slot, out InventoryEquipmentSlotUI equipment);

            if (equipment == null || equipment.slot == null)
            {
                return;
            }

            if (newItem.isDefaultItem)
            {
                equipment.slot.ClearSlot();
            }
            else
            {
                equipment.slot.AddItem(newInventoryItem);
            }
        }
    }
}
