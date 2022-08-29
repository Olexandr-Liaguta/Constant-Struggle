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
    InventoryStatsManagerUI inventoryStatsManager;

    public Dictionary<ItemRarity, float> rarityChance = new()
    {
        {ItemRarity.Improved, 50},
        {ItemRarity.Magical, 30},
        {ItemRarity.Rare, 5},
        {ItemRarity.Demonic, 1},
        {ItemRarity.Set, 0.1f},
    };

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChange;

        inventoryStatsManager.UpdateStats(this);
    }

    void OnEquipmentChange(EquipmentSlotExact slot, InventoryItem newItem, InventoryItem oldItem)
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

    private void AddModifiers(InventoryItem inventoryItem)
    {
        var modifiersMap = (inventoryItem.item as Equipment).modifiersMap;

        if (modifiersMap == null) return;

        foreach (var modifier in modifiersMap)
        {
            switch (modifier.Key)
            {
                case Modifier.Armor:
                    armor.AddModifier(modifier.Value);
                    break;

                case Modifier.MinDamage:
                    minDamage.AddModifier(modifier.Value);
                    break;

                case Modifier.MaxDamage:
                    maxDamage.AddModifier(modifier.Value);
                    break;

                case Modifier.AttackSpeed:
                    attackSpeed.AddModifier(modifier.Value);
                    break;
            }
        }
    }

    private void RemoveModifiers(InventoryItem inventoryItem)
    {
        var modifiersMap = (inventoryItem.item as Equipment).modifiersMap;

        if (modifiersMap == null) return;

        foreach (var modifier in modifiersMap)
        {
            switch (modifier.Key)
            {
                case Modifier.Armor:
                    armor.RemoveModifier(modifier.Value);
                    break;

                case Modifier.MinDamage:
                    minDamage.RemoveModifier(modifier.Value);
                    break;

                case Modifier.MaxDamage:
                    maxDamage.RemoveModifier(modifier.Value);
                    break;

                case Modifier.AttackSpeed:
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

        healthText.text = currentHealth + " / " + GetMaxHealth();
    }

    public override void Die()
    {
        base.Die();

        PlayerManager.instance.KillPlayer();
    }

}
