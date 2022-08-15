using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ring Equipment", menuName = "Inventory/Ring Equipment")]
public class RingEquipment : Equipment
{
    public int armor;

    public override void OnLaunch()
    {
        modifiersMap.Add(EquipmentModifier.Armor, armor);
    }
}
