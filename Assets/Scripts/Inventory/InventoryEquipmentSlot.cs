using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEquipmentSlot : MonoBehaviour
{
   public EquipmentSlot equipmentSlot;

    [HideInInspector]
    public InventorySlot slot;

    private void Start()
    {
        slot = GetComponentInChildren<InventorySlot>();
    }

}
