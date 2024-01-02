using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryEquipmentUI : MonoBehaviour
{
    [SerializeField]
    private List<InventoryEquipmentSlotUI> equipmentSlotUIs = new();

    void Start()
    {
        EquipmentManager.Instance.OnEquipmentChanged += UpdateEquipmentUI;
    }

    void UpdateEquipmentUI(object sender, EventArgs args)
    {
        foreach (var equipmentSlot in equipmentSlotUIs)
        {
            equipmentSlot.inventorySlotUI.ClearSlot();
        }

        var equipments = PlayerInventoryData.GetEquipments();

        foreach (var equip in equipments)
        {
            if (equip.inventoryItem == null) continue;

            int index = equipmentSlotUIs.FindIndex(equipSlotUI => equipSlotUI.equipmentSlot == equip.slot);
            if (index >= 0)
            {
                equipmentSlotUIs[index].inventorySlotUI.SetItem(equip.inventoryItem);
            }
        }
    }
}
