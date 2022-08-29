using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield Equipment", menuName = "Inventory/Equipment/Shield Equipment")]
public class ShieldEquipment : Equipment
{
    public int armor;

    public override void OnLaunch()
    {
        modifiersMap.Add(Modifier.Armor, armor);
    }
}
