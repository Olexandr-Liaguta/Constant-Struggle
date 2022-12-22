using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Equipment", menuName = "Inventory/Equipment/Weapon Equipment")]
public class WeaponEquipment : Equipment
{
    public int minDamage;
    public int maxDamage;
    public int attackSpeed;

    public override void OnLaunch()
    {
        modifiersMap.Add(Modifier.Damage, new ModifierValue(min: minDamage, max: maxDamage));
        modifiersMap.Add(Modifier.AttackSpeed, new ModifierValue(attackSpeed));
    }
}
