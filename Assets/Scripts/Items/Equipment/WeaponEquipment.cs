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
        modifiersMap.Add(Modifier.MinDamage, minDamage);
        modifiersMap.Add(Modifier.MaxDamage, maxDamage);
        modifiersMap.Add(Modifier.AttackSpeed, attackSpeed);
    }
}
