using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryStatsManager : MonoBehaviour
{
    InventoryArmorStat armorStat;
    InventoryDamageStat damageStat;
    InventoryPrimalStat[] primalStats; 


    void Start()
    {
        armorStat = GetComponentInChildren<InventoryArmorStat>();   
        damageStat = GetComponentInChildren<InventoryDamageStat>();   
        primalStats = GetComponentsInChildren<InventoryPrimalStat>();   
    }

    public void UpdateStats(PlayerStats playerStats)
    {
        armorStat.SetStat(playerStats.armor.GetValue());

        var damages = playerStats.GetCalculatedDamages();
        damageStat.SetStats(damages.minDamage, damages.maxDamage);

        foreach(var primalStat in primalStats)
        {
            switch(primalStat.statType)
            {
                case PrimalStat.Accuracy:
                    primalStat.SetStats(playerStats.accuracy.GetBaseValue(), playerStats.accuracy.GetValue());
                    break;
                case PrimalStat.Agility:
                    primalStat.SetStats(playerStats.agility.GetBaseValue(), playerStats.agility.GetValue());
                    break;
                case PrimalStat.Spirit:
                    primalStat.SetStats(playerStats.spirit.GetBaseValue(), playerStats.spirit.GetValue());
                    break;
                case PrimalStat.Strength:
                    primalStat.SetStats(playerStats.strength.GetBaseValue(), playerStats.strength.GetValue());
                    break;
            }
        }
    }

}
