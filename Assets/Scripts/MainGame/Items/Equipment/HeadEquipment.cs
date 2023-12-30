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
        addModifiers.Add(new ItemManager.AddModifier() { attribute = Attribute.Armor, value = new ModifierValue(armor) });
    }
}
