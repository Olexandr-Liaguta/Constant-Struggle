using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chest Equipment", menuName = "Inventory/Chest Equipment")]
public class ChestEquipment : Equipment
{
    public int armor;

    public override void OnLaunch()
    {
        modifiersMap.Add(EquipmentModifier.Armor, armor);
    }
}
