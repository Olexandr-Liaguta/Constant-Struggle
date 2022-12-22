using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


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
    public EquipmentSlot equipmentSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public Dictionary<Modifier, ModifierValue> modifiersMap = new();
}

public enum EquipmentSlot { Head, Chest, Legs, Hand, Feet, Gloves, Ring, Amulet }
public enum EquipmentMeshRegion { Legs, Arms, Torso }