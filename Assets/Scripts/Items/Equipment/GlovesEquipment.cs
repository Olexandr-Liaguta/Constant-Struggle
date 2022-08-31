using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gloves Equipment", menuName = "Inventory/Equipment/Gloves Equipment")]
public class GlovesEquipment : Equipment
{
    public int armor;

    public override void OnLaunch()
    {
        modifiersMap.Add(Modifier.Armor, new ModifierValue(armor));
    }
}
