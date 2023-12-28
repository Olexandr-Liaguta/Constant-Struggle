using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEquipmentUI : MonoBehaviour
{
    public Transform equipmentsParent;

    Dictionary<EquipmentSlotExact, InventoryEquipmentSlotUI> equipmentSlots = new();


    void Start()
    {
        EquipmentManager.instance.OnEquipmentChanged += UpdateEquipmentUI;


        var equipmentSlotsInChildrens = equipmentsParent.GetComponentsInChildren<InventoryEquipmentSlotUI>();

        foreach (var equipmentSlot in equipmentSlotsInChildrens)
        {
            equipmentSlots.Add(equipmentSlot.equipmentSlot, equipmentSlot);
        }
    }


    void UpdateEquipmentUI(object sender, EquipmentManager.OnEquipmentChangedArgs args)
    {
        if (args.newItem == null)
        {
            equipmentSlots.TryGetValue(args.slot, out InventoryEquipmentSlotUI equipment);

            if (equipment == null || equipment.slot == null)
            {
                return;
            }

            equipment.slot.ClearSlot();
        }
        else
        {
            Equipment newItem = args.newItem.item as Equipment;

            equipmentSlots.TryGetValue(args.slot, out InventoryEquipmentSlotUI equipment);

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
                equipment.slot.AddItem(args.newItem);
            }
        }
    }
}
