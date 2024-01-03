using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlotExact { Head, Chest, Legs, Hand_1, Hand_2, Feet, Gloves, Ring_1, Ring_2, Amulet }

public enum EquipmentSlot { Head, Chest, Legs, Hand, Feet, Gloves, Ring, Amulet }


public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance { get; private set; }


    public event EventHandler OnEquipmentChanged;

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



    void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        SaveManager.Instance.OnLoadGame += SaveManager_OnLoadGame;

        defaultEquipment[EquipmentSlotExact.Head] = new InventoryItem(headDefaultEquipment);
        defaultEquipment[EquipmentSlotExact.Chest] = new InventoryItem(chestDefaultEquipment);
        defaultEquipment[EquipmentSlotExact.Legs] = new InventoryItem(legsDefaultEquipment);

        _EquipDefaultItems();
    }

    private void SaveManager_OnLoadGame(object sender, EventArgs e)
    {
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    EquipmentSlotExact? _HandleEquipmentSlot(EquipmentSlot equipmentSlot)
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

        return null;
    }

    EquipmentSlotExact _GetAvailableHandEquipment()
    {
        if (PlayerInventoryData.GetInventoryItemFromEquipment(EquipmentSlotExact.Hand_1) == null)
        {
            return EquipmentSlotExact.Hand_1;
        }

        return EquipmentSlotExact.Hand_2;
    }

    EquipmentSlotExact _GetAvailableRingEquipment()
    {
        if (PlayerInventoryData.GetInventoryItemFromEquipment(EquipmentSlotExact.Ring_1) == null)
        {
            return EquipmentSlotExact.Ring_1;
        }

        return EquipmentSlotExact.Ring_2;
    }

    public void Equip(InventoryItem newInventoryItem)
    {
        Equipment newItem = newInventoryItem.item as Equipment;

        _SetEquipmentBlendShapes(newItem, 100);

        EquipmentSlotExact? equipmentSlotNullable = _HandleEquipmentSlot(newItem.equipmentSlot); ;

        if (equipmentSlotNullable != null)
        {
            EquipmentSlotExact equipmentSlot = (EquipmentSlotExact)equipmentSlotNullable;

            if (newItem.mesh)
            {
                _HandleMesh(newItem.mesh, equipmentSlot);
            }

            if (!newInventoryItem.item.isDefaultItem)
            {
                InventoryItem oldInventoryItem = Unequip(equipmentSlot);

                PlayerInventoryData.SetEquipmentSlot(equipmentSlot, newInventoryItem);

                OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
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
        InventoryItem oldItem = PlayerInventoryData.GetInventoryItemFromEquipment(equipmentSlot);

        if (oldItem == null) return null;

        if (currentMeshes[equipmentSlot] != null)
        {
            Destroy(currentMeshes[equipmentSlot].gameObject);
        }

        PlayerInventoryManager.Instance.Add(oldItem);

        _SetEquipmentBlendShapes(oldItem.item as Equipment, 0);

        PlayerInventoryData.SetEquipmentSlot(equipmentSlot, null);

        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);


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
}
