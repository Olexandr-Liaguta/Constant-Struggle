using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gloves Equipment", menuName = "Inventory/Equipment/Gloves Equipment")]
public class GlovesEquipment : Equipment
{
    public int armor;

    public override void OnLaunch()
    {
        addModifiers.Add(new ItemManager.AddModifier() { attribute = Attribute.Armor, value = new ModifierValue(armor) });
    }
}
