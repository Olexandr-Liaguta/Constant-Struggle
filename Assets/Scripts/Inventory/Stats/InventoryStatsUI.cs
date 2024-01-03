using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryStatsUI : MonoBehaviour
{
    [SerializeField] private InventoryStatUI armorStat, damageStat, accuracyStat, agilityStat, spiritStat, strengthStat;

    private void Start()
    {
        Debug.Log("InventoryStatsUI Subscribe on OnStatsChange");
        PlayerStats.Instance.OnStatsChange += PlayerStats_OnStatsChange;
    }

    private void PlayerStats_OnStatsChange(object sender, System.EventArgs e)
    {
        Debug.Log("PlayerStats_OnStatsChange");
        UpdateStats();
    }

    public void UpdateStats()
    {
        PlayerStats playerStats = PlayerStats.Instance;

        armorStat.SetStats(playerStats.armor.GetValue().ToString());

        var damages = playerStats.GetCalculatedDamages();
        string damageString = damages.minDamage + " - " + damages.maxDamage;
        damageStat.SetStats(damageString);

        SetStat(accuracyStat, playerStats.accuracy);
        SetStat(agilityStat, playerStats.agility);
        SetStat(strengthStat, playerStats.strength);
        SetStat(spiritStat, playerStats.spirit);
    }

    private void SetStat(InventoryStatUI inventoryStatUI, Stat stat) 
    {
        inventoryStatUI.SetStats(stat.GetValue().ToString(), "(" + stat.GetBaseValue() + ")");
    }

}
