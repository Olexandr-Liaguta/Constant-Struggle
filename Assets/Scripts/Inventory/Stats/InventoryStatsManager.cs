using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryStatsManager : MonoBehaviour
{
    [SerializeField]
    InventoryArmorStat armorStat;

    [SerializeField]
    InventoryDamageStat damageStat;

    [SerializeField]
    InventoryPrimalStat accuracyStat, agilityStat, spiritStat, strengthStat; 


    public void UpdateStats(PlayerStats playerStats)
    {
        armorStat.SetStat(playerStats.armor.GetValue());

        var damages = playerStats.GetCalculatedDamages();
        damageStat.SetStats(damages.minDamage, damages.maxDamage);

        accuracyStat.SetStats(playerStats.accuracy.GetBaseValue(), playerStats.accuracy.GetValue());
        agilityStat.SetStats(playerStats.agility.GetBaseValue(), playerStats.agility.GetValue());
        spiritStat.SetStats(playerStats.spirit.GetBaseValue(), playerStats.spirit.GetValue());
        strengthStat.SetStats(playerStats.strength.GetBaseValue(), playerStats.strength.GetValue());
    }

}
