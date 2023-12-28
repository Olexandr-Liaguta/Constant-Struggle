using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlotExact { Head, Chest, Legs, Hand_1, Hand_2, Feet, Gloves, Ring_1, Ring_2, Amulet, None }

public class EquipmentManager : MonoBehaviour
{
    #region Singelton

    public static EquipmentManager instance { get; private set; }

    void Awake()
    {
        instance = this;
    }

    #endregion

    public event EventHandler<OnEquipmentChangedArgs> OnEquipmentChanged;
    public class OnEquipmentChangedArgs : EventArgs
    {
        public EquipmentSlotExact slot;
        public InventoryItem newItem;
        public InventoryItem oldItem;
    }



    [SerializeField] Equipment headDefaultEquipment, legsDefaultEquipment, chestDefaultEquipment;
    public SkinnedMeshRenderer targetMesh;

    Dictionary<EquipmentSlotExact, InventoryItem> defaultEquipment = new() {
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

    Dictionary<EquipmentSlotExact, InventoryItem> currentEquipment = new() {
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




    private void Start()
    {
        defaultEquipment[EquipmentSlotExact.Head] = new InventoryItem(headDefaultEquipment);
        defaultEquipment[EquipmentSlotExact.Chest] = new InventoryItem(chestDefaultEquipment);
        defaultEquipment[EquipmentSlotExact.Legs] = new InventoryItem(legsDefaultEquipment);

        _EquipDefaultItems();
    }

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.U)) UnequipAll();
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
        if (currentEquipment[EquipmentSlotExact.Hand_1] == null)
        {
            return EquipmentSlotExact.Hand_1;
        }

        return EquipmentSlotExact.Hand_2;
    }

    EquipmentSlotExact _GetAvailableRingEquipment()
    {
        if (currentEquipment[EquipmentSlotExact.Ring_1] == null)
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

            currentEquipment[equipmentSlot] = newInventoryItem;

            OnEquipmentChanged?.Invoke(this, new OnEquipmentChangedArgs
            {
                slot = equipmentSlot,
                newItem = newInventoryItem,
                oldItem = null,
            });

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
        InventoryItem oldItem = currentEquipment[equipmentSlot];

        if (oldItem == null) return null;

        if (currentMeshes[equipmentSlot] != null)
        {
            Destroy(currentMeshes[equipmentSlot].gameObject);
        }

        PlayerInventoryManager.instance.Add(oldItem);

        _SetEquipmentBlendShapes(oldItem.item as Equipment, 0);

        currentEquipment[equipmentSlot] = null;

        OnEquipmentChanged?.Invoke(this, new OnEquipmentChangedArgs
        {
            slot = equipmentSlot,
            newItem = null,
            oldItem = oldItem,
        });

        var defaultEquipment = this.defaultEquipment[equipmentSlot];

        if (defaultEquipment != null)
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
        foreach (var inventoryItem in currentEquipment)
        {
            Unequip(inventoryItem.Key);
        }
        _EquipDefaultItems();
    }

    void _EquipDefaultItems()
    {
        foreach (InventoryItem item in defaultEquipment.Values)
        {
            if (item != null)
            {
                Equip(item);
            }
        }
    }

    public EquipmentSlotExact? GetEquipmentSlot(InventoryItem inventoryItem)
    {
        foreach (var equipment in currentEquipment)
        {
            if (equipment.Value?.id == inventoryItem.id)
            {
                return equipment.Key;
            }
        }

        return null;
    }
}
