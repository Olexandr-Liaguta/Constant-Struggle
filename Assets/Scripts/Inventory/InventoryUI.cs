using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using UnityEngine;
using Cinemachine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public Transform equipmentsParent;
    public GameObject inventoryUI_GO;

    [SerializeField]
    private CinemachineFreeLook camera;

    Inventory inventory;
    InventorySlot[] slots;
    Guid selectedInventoryId;

    EquipmentManager equipmentManager;
    Dictionary<EquipmentSlot, InventoryEquipmentSlot> equipmentSlots = new();


    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemsChangedCallback += UpdateUI;

        equipmentManager = EquipmentManager.instance;
        equipmentManager.onEquipmentChanged += UpdateEquipmentUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        var equipmentSlotsInChildrens = equipmentsParent.GetComponentsInChildren<InventoryEquipmentSlot>();

        foreach (var equipmentSlot in equipmentSlotsInChildrens)
        {
            equipmentSlots.Add(equipmentSlot.equipmentSlot, equipmentSlot);
        }

        UpdateUI();

        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            Debug.Log("Inventory " + inventoryUI_GO.activeSelf);

            if (inventoryUI_GO.activeSelf)
            {
                inventoryUI_GO.SetActive(false);
                camera.enabled = true;
                Cursor.visible = false;
            }
            else
            {
                inventoryUI_GO.SetActive(true);
                camera.enabled = false;
                Cursor.visible = true;
            }

        }
    }

    public void SelectInventoryItem(Guid id)
    {
        selectedInventoryId = id;
        UpdateUI();
    }

    void UpdateUI()
    {
        Guid id;

        for (int i = 0; i < slots.Length; i++)
        {
            id = slots[i].id;

            slots[i].SetSelectedState(id.Equals(selectedInventoryId));

            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    void UpdateEquipmentUI(Equipment newItem, Equipment oldItem)
    {
        if (newItem == null) return;

        equipmentSlots.TryGetValue(newItem.equipmentSlot, out InventoryEquipmentSlot equipment);

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
            equipment.slot.AddItem(newItem);
        }
    }


}
