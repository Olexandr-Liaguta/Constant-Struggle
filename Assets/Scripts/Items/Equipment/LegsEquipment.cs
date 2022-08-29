using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Legs Equipment", menuName = "Inventory/Equipment/LegsEquipment")]
public class LegsEquipment : Equipment
{
    public int armor;

    public override void OnLaunch()
    {
        modifiersMap.Add(Modifier.Armor, armor);
    }
}
