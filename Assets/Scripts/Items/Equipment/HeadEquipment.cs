using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName = "Head Equipment", menuName = "Inventory/Equipment/Head Equipment")]
public class HeadEquipment : Equipment
{
    public int armor;

    public override void OnLaunch()
    {
        modifiersMap.Add(Modifier.Armor, armor);
    }
}
