using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ModifierValue
{
    public int value;

    public int min;
    public int max;

    public ModifierValue(int value)
    {
        this.value = value;
    }

    public ModifierValue(int min, int max)
    {
        this.min = min;
        this.max = max;
    }
}

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment/Equipment")]
public class Equipment : Item
{
    public enum EquipmentMeshRegion { Legs, Arms, Torso }

    public EquipmentSlot equipmentSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public List<ItemManager.AddModifier> addModifiers = new();
}

