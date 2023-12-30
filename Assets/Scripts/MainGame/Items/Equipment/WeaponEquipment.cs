using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Equipment", menuName = "Inventory/Equipment/Weapon Equipment")]
public class WeaponEquipment : Equipment
{
    public int minDamage;
    public int maxDamage;
    public int attackSpeed;

    public override void OnLaunch()
    {
        addModifiers.Add(new ItemManager.AddModifier() { attribute = Attribute.Damage, value = new ModifierValue(min: minDamage, max: maxDamage) });
        addModifiers.Add(new ItemManager.AddModifier() { attribute = Attribute.AttackSpeed, value = new ModifierValue(attackSpeed) });
    }
}
