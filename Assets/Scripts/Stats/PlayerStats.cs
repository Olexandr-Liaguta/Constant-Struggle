using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [SerializeField]
    HealthBarUI healthBarUI;

    [SerializeField]
    ManaBarUI manaBarUI;

    [SerializeField]
    InventoryStatsManagerUI inventoryStatsManager;

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChange;

        inventoryStatsManager.UpdateStats(this);
    }

    void OnEquipmentChange(EquipmentSlotExact slot, InventoryItem newItem, InventoryItem oldItem)
    {
        if (newItem != null)
        {
            _AddModifiers(newItem);
        }

        if (oldItem != null)
        {
            _RemoveModifiers(oldItem);
        }

        _UpdatePointStats();
        inventoryStatsManager.UpdateStats(this);

        OnChangeHealth();
        OnChangeMana();
    }

    private void _AddModifiers(InventoryItem inventoryItem)
    {
        var modifiersMap = (inventoryItem.item as Equipment).modifiersMap;

        if (modifiersMap != null)
        {
            _HandleAddModifierMap(modifiersMap);
        }

        var addModifiersMap = inventoryItem.addModifiers;

        if (addModifiersMap != null)
        {
            _HandleAddModifierMap(addModifiersMap);
        }
    }

    void _HandleAddModifierMap(Dictionary<Modifier, int> modifiersMap)
    {
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
                case Modifier.Health:
                    health.AddModifier(modifier.Value);
                    break;
                case Modifier.Mana:
                    mana.AddModifier(modifier.Value);
                    break;
            }
        }
    }

    private void _RemoveModifiers(InventoryItem inventoryItem)
    {
        var modifiersMap = (inventoryItem.item as Equipment).modifiersMap;

        if (modifiersMap != null)
        {
            _HandleRemoveModifiersMap(modifiersMap);
        }

        var addModifiersMap = inventoryItem.addModifiers;

        if (addModifiersMap != null)
        {
            _HandleRemoveModifiersMap(addModifiersMap);
        }
    }

    void _HandleRemoveModifiersMap(Dictionary<Modifier, int> modifiersMap)
    {
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
                case Modifier.Health:
                    health.RemoveModifier(modifier.Value);
                    break;
                case Modifier.Mana:
                    mana.RemoveModifier(modifier.Value);
                    break;
            }
        }
    }

    void _UpdatePointStats()
    {
        health.SetStatModifier(strength.GetValue());
    }

    public override void OnChangeHealth()
    {
        base.OnChangeHealth();

        healthBarUI.UpdateHealth(current: health.currentValue, max: GetMaxHealth());
    }

    public override void OnChangeMana()
    {
        base.OnChangeMana();

        manaBarUI.UpdateMana(current: mana.currentValue, max: mana.GetMaxValue());
    }

    public override void Die()
    {
        base.Die();

        PlayerManager.instance.KillPlayer();
    }

}
