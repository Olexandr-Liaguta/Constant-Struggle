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
        if (oldItem != null)
        {
            _RemoveModifiers(oldItem);
        }

        if (newItem != null)
        {
            _AddModifiers(newItem);
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

    void _HandleAddModifierMap(Dictionary<Modifier, ModifierValue> modifiersMap)
    {
        foreach (var modifier in modifiersMap)
        {
            switch (modifier.Key)
            {
                case Modifier.Armor:
                    armor.AddModifier(modifier.Value.value);
                    break;

                case Modifier.Damage:
                    minDamage.AddModifier(modifier.Value.min);
                    maxDamage.AddModifier(modifier.Value.max);
                    break;

                case Modifier.AttackSpeed:
                    attackSpeed.AddModifier(modifier.Value.value);
                    break;

                case Modifier.Health:
                    health.AddModifier(modifier.Value.value);
                    break;

                case Modifier.Mana:
                    mana.AddModifier(modifier.Value.value);
                    break;
                
                case Modifier.Accuracy:
                    accuracy.AddModifier(modifier.Value.value);
                    break;
                
                case Modifier.Agility:
                    agility.AddModifier(modifier.Value.value);
                    break;
                    
                case Modifier.Spirit:
                    spirit.AddModifier(modifier.Value.value);
                    break;
                
                case Modifier.Strength:
                    strength.AddModifier(modifier.Value.value);
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

    void _HandleRemoveModifiersMap(Dictionary<Modifier, ModifierValue> modifiersMap)
    {
        foreach (var modifier in modifiersMap)
        {
            switch (modifier.Key)
            {
                case Modifier.Armor:
                    armor.RemoveModifier(modifier.Value.value);
                    break;

                case Modifier.Damage:
                    minDamage.RemoveModifier(modifier.Value.min);
                    maxDamage.RemoveModifier(modifier.Value.max);
                    break;

                case Modifier.AttackSpeed:
                    attackSpeed.RemoveModifier(modifier.Value.value);
                    break;

                case Modifier.Health:
                    health.RemoveModifier(modifier.Value.value);
                    break;

                case Modifier.Mana:
                    mana.RemoveModifier(modifier.Value.value);
                    break;

                case Modifier.Accuracy:
                    accuracy.RemoveModifier(modifier.Value.value);
                    break;

                case Modifier.Agility:
                    agility.RemoveModifier(modifier.Value.value);
                    break;

                case Modifier.Spirit:
                    spirit.RemoveModifier(modifier.Value.value);
                    break;

                case Modifier.Strength:
                    strength.RemoveModifier(modifier.Value.value);
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
