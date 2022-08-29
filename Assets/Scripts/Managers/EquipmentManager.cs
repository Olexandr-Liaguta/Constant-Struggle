using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlotExact { Head, Chest, Legs, Hand_1, Hand_2, Feet, Gloves, Ring_1, Ring_2, Amulet, None }

public class EquipmentManager : MonoBehaviour
{
    #region Singelton

    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion


    [SerializeField] private Equipment[] defaultEquipments;

    [SerializeField] Equipment headDefaultEquipment, legsDefaultEquipment, chestDefaultEquipment;

    Dictionary<EquipmentSlotExact, InventoryItem> _defaultEquipment = new() {
        {EquipmentSlotExact.Ring_1, null },
        {EquipmentSlotExact.Hand_1, null },
        {EquipmentSlotExact.Head, null },
        {EquipmentSlotExact.Hand_2, null },
        {EquipmentSlotExact.Ring_2, null },
        {EquipmentSlotExact.Amulet, null },
        {EquipmentSlotExact.Gloves, null },
        {EquipmentSlotExact.Chest, null },
        {EquipmentSlotExact.Legs, null },
        {EquipmentSlotExact.Feet, null },
    };

    public SkinnedMeshRenderer targetMesh;

    Dictionary<EquipmentSlotExact, InventoryItem> _currentEquipment = new() {
        {EquipmentSlotExact.Ring_1, null },
        {EquipmentSlotExact.Hand_1, null },
        {EquipmentSlotExact.Head, null },
        {EquipmentSlotExact.Hand_2, null },
        {EquipmentSlotExact.Ring_2, null },
        {EquipmentSlotExact.Amulet, null },
        {EquipmentSlotExact.Gloves, null },
        {EquipmentSlotExact.Chest, null },
        {EquipmentSlotExact.Legs, null },
        {EquipmentSlotExact.Feet, null },
    };

    Dictionary<EquipmentSlotExact, SkinnedMeshRenderer> currentMeshes = new() {
        {EquipmentSlotExact.Ring_1, null },
        {EquipmentSlotExact.Hand_1, null },
        {EquipmentSlotExact.Head, null },
        {EquipmentSlotExact.Hand_2, null },
        {EquipmentSlotExact.Ring_2, null },
        {EquipmentSlotExact.Amulet, null },
        {EquipmentSlotExact.Gloves, null },
        {EquipmentSlotExact.Chest, null },
        {EquipmentSlotExact.Legs, null },
        {EquipmentSlotExact.Feet, null },
    };

    public delegate void OnEquipmentChanged(EquipmentSlotExact slot, InventoryItem newItem, InventoryItem oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;

        _defaultEquipment[EquipmentSlotExact.Head] = new InventoryItem(headDefaultEquipment);
        _defaultEquipment[EquipmentSlotExact.Chest] = new InventoryItem(chestDefaultEquipment);
        _defaultEquipment[EquipmentSlotExact.Legs] = new InventoryItem(legsDefaultEquipment);

        _EquipDefaultItems();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) UnequipAll();
    }

    EquipmentSlotExact _HandleEquipmentSlot(EquipmentSlot equipmentSlot)
    {
        switch (equipmentSlot)
        {
            case EquipmentSlot.Amulet: return EquipmentSlotExact.Amulet;
            case EquipmentSlot.Chest: return EquipmentSlotExact.Chest;
            case EquipmentSlot.Feet: return EquipmentSlotExact.Feet;
            case EquipmentSlot.Gloves: return EquipmentSlotExact.Gloves;
            case EquipmentSlot.Hand: return _GetAvailableHandEquipment();
            case EquipmentSlot.Head: return EquipmentSlotExact.Head;
            case EquipmentSlot.Legs: return EquipmentSlotExact.Legs;
            case EquipmentSlot.Ring: return _GetAvailableRingEquipment();
        }

        return EquipmentSlotExact.None;
    }

    EquipmentSlotExact _GetAvailableHandEquipment()
    {
        if (_currentEquipment[EquipmentSlotExact.Hand_1] == null)
        {
            return EquipmentSlotExact.Hand_1;
        }

        return EquipmentSlotExact.Hand_2;
    }

    EquipmentSlotExact _GetAvailableRingEquipment()
    {
        if (_currentEquipment[EquipmentSlotExact.Ring_1] == null)
        {
            return EquipmentSlotExact.Ring_1;
        }

        return EquipmentSlotExact.Ring_2;
    }

    public void Equip(InventoryItem newInventoryItem)
    {
        var newItem = newInventoryItem.item as Equipment;

        EquipmentSlotExact equipmentSlot = _HandleEquipmentSlot(newItem.equipmentSlot);

        _SetEquipmentBlendShapes(newItem, 100);

        if (newItem.mesh)
        {
            _HandleMesh(newItem.mesh, equipmentSlot);
        }

        if (!newInventoryItem.item.isDefaultItem)
        {
            InventoryItem oldInventoryItem = Unequip(equipmentSlot);

            _currentEquipment[equipmentSlot] = newInventoryItem;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(equipmentSlot, newInventoryItem, oldInventoryItem);
            }

        }
    }

    void _HandleMesh(SkinnedMeshRenderer mesh, EquipmentSlotExact equipmentSlot)
    {
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(mesh);
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[equipmentSlot] = newMesh;
    }

    public InventoryItem Unequip(EquipmentSlotExact equipmentSlot)
    {
        InventoryItem oldItem = _currentEquipment[equipmentSlot];

        if (oldItem == null) return null;

        if (currentMeshes[equipmentSlot] != null)
        {
            Destroy(currentMeshes[equipmentSlot].gameObject);
        }

        inventory.Add(oldItem);

        _SetEquipmentBlendShapes(oldItem.item as Equipment, 0);

        _currentEquipment[equipmentSlot] = null;

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(equipmentSlot, null, oldItem);
        }

        var defaultEquipment = _defaultEquipment[equipmentSlot];

        if(defaultEquipment != null)
        {
            Equip(defaultEquipment);
        }

        return oldItem;
    }

    void _SetEquipmentBlendShapes(Equipment item, int weight)
    {
        foreach (var blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    public void UnequipAll()
    {
        foreach (var inventoryItem in _currentEquipment)
        {
            Unequip(inventoryItem.Key);
        }
        _EquipDefaultItems();
    }

    void _EquipDefaultItems()
    {
        foreach (InventoryItem item in _defaultEquipment.Values)
        {
            if (item != null)
            {
                Equip(item);
            }
        }
    }

    public EquipmentSlotExact? IsEquiped(InventoryItem inventoryItem)
    {
        foreach (var equipment in _currentEquipment)
        {
            if (equipment.Value?.id == inventoryItem.id)
            {
                return equipment.Key;
            }
        }

        return null;
    }
}
