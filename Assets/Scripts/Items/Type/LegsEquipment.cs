using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Legs Equipment", menuName = "Inventory/LegsEquipment")]
public class LegsEquipment : Equipment
{
    public int armor;

    public override void OnLaunch()
    {
        modifiersMap.Add(EquipmentModifier.Armor, armor);
    }
}
