using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : CharacterStats
{
    [SerializeField]
    private Image healthImage;

    [SerializeField]
    private TextMeshProUGUI healthText;

    [SerializeField]
    InventoryStatsManager inventoryStatsManager;

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChange;

        inventoryStatsManager.UpdateStats(this);
    }

    void OnEquipmentChange(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            AddModifiers(newItem);
        }

        if (oldItem != null)
        {
            RemoveModifiers(oldItem);
        }

        inventoryStatsManager.UpdateStats(this);
    }

    private void AddModifiers(Equipment equipment)
    {
        if (equipment.modifiersMap == null) return;

        foreach (var modifier in equipment.modifiersMap)
        {
            switch(modifier.Key)
            {
                case EquipmentModifier.Armor:
                    armor.AddModifier(modifier.Value);
                    break;

                case EquipmentModifier.MinDamage:
                    minDamage.AddModifier(modifier.Value);
                    break;
                
                case EquipmentModifier.MaxDamage:
                    maxDamage.AddModifier(modifier.Value);
                    break;
                
                case EquipmentModifier.AttackSpeed:
                    attackSpeed.AddModifier(modifier.Value);
                    break;
            }
        }
    }
    
    private void RemoveModifiers(Equipment equipment)
    {
        if (equipment.modifiersMap == null) return;

        foreach(var modifier in equipment.modifiersMap)
        {
            switch(modifier.Key)
            {
                case EquipmentModifier.Armor:
                    armor.RemoveModifier(modifier.Value);
                    break;

                case EquipmentModifier.MinDamage:
                    minDamage.RemoveModifier(modifier.Value);
                    break;
                
                case EquipmentModifier.MaxDamage:
                    maxDamage.RemoveModifier(modifier.Value);
                    break;
                
                case EquipmentModifier.AttackSpeed:
                    attackSpeed.RemoveModifier(modifier.Value);
                    break;
            }
        }
    }

    public override void OnChangeHealth()
    {
        base.OnChangeHealth();

        float healthPercent = (float)currentHealth / (float)GetMaxHealth();

        healthImage.fillAmount = healthPercent;

        healthText.text = GetMaxHealth() + " / " + currentHealth;
    }

    public override void Die()
    {
        base.Die();

        PlayerManager.instance.KillPlayer();
    }

}
