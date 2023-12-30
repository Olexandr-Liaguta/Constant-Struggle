using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Amulet Equipment", menuName = "Inventory/Equipment/Amulet Equipment")]
public class AmuletEquipment : Equipment
{
    public int armor;

    public override void OnLaunch()
    {
        addModifiers.Add(new ItemManager.AddModifier() { attribute = Attribute.Armor, value = new ModifierValue(armor) });
    }
}
