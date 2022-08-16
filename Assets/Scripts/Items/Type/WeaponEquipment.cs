using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquipment : Equipment
{
    public int minDamage;
    public int maxDamage;
    public int attackSpeed;

    public override void OnLaunch()
    {
        modifiersMap.Add(EquipmentModifier.MinDamage, minDamage);
        modifiersMap.Add(EquipmentModifier.MaxDamage, maxDamage);
        modifiersMap.Add(EquipmentModifier.AttackSpeed, attackSpeed);
    }
}
