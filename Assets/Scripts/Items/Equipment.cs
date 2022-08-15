using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipmentSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public Dictionary<EquipmentModifier, int> modifiersMap = new();

    public override void Use()
    {
        base.Use();

        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentModifier
{
    MinDamage,
    MaxDamage,
    AttackSpeed,
    Armor,
}

public enum EquipmentSlot { Head, Chest, Legs, Hand1, Hand2, Feet, Gloves, Ring1, Ring2, Amulet }
public enum EquipmentMeshRegion { Legs, Arms, Torso }