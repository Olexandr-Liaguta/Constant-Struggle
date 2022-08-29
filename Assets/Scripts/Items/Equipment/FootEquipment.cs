using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Foot Equipment", menuName = "Inventory/Equipment/Foot Equipment")]
public class FootEquipment : Equipment
{
    public int armor;

    public override void OnLaunch()
    {
        modifiersMap.Add(Modifier.Armor, armor);
    }
}
