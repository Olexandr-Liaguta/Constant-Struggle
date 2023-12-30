using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Foot Equipment", menuName = "Inventory/Equipment/Foot Equipment")]
public class FootEquipment : Equipment
{
    public int armor;

    public override void OnLaunch()
    {
        addModifiers.Add(new ItemManager.AddModifier() { attribute = Attribute.Armor, value = new ModifierValue(armor) });
    }
}
