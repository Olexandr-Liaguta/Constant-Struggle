using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipmentSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public Dictionary<Modifier, int> modifiersMap = new();
}

public enum EquipmentSlot { Head, Chest, Legs, Hand, Feet, Gloves, Ring, Amulet }
public enum EquipmentMeshRegion { Legs, Arms, Torso }