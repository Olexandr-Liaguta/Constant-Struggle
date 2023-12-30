using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield Equipment", menuName = "Inventory/Equipment/Shield Equipment")]
public class ShieldEquipment : Equipment
{
    public int armor;

    public override void OnLaunch()
    {
        addModifiers.Add(new ItemManager.AddModifier() { attribute = Attribute.Armor, value = new ModifierValue(armor) });
    }
}
