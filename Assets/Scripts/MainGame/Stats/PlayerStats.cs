using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public static PlayerStats Instance { get; private set; }



    public event EventHandler<OnHealthChangeArgs> OnHealthChange;
    public class OnHealthChangeArgs: EventArgs
    {
        public float health;
        public float maxHealth;
    }

    public event EventHandler<OnManaChangeArgs> OnManaChange;
    public class OnManaChangeArgs : EventArgs
    {
        public float mana;
        public float maxMana;
    }

    public event EventHandler OnStatsChange;



    private void Awake()
    {
        Instance = this;
    }

    new void Start()
    {
        base.Start();

        EquipmentManager.Instance.OnEquipmentChanged += EquipmentManager_OnEquipmentChanged; ;

        OnHealthChange?.Invoke(this, new OnHealthChangeArgs { health = health.currentValue, maxHealth = GetMaxHealth() });
        OnManaChange?.Invoke(this, new OnManaChangeArgs { mana = mana.currentValue, maxMana = mana.GetMaxValue() });
        OnStatsChange?.Invoke(this, EventArgs.Empty);
    }

    private void EquipmentManager_OnEquipmentChanged(object sender, EventArgs e)
    {
        UpdateStats();
    }

    void UpdateStats()
    {
        RemoveAllStatsModifiers();

        var equipments = PlayerInventoryData.GetEquipments();

        foreach(var equip in equipments)
        {
            var equipmentModifiers = (equip.inventoryItem?.item as Equipment)?.addModifiers;

            if (equipmentModifiers != null)
            {
                UpdateStatFromModifiers(equipmentModifiers);
            }

            var inventoryModifiers = equip.inventoryItem?.addModifiers;

            if (inventoryModifiers != null)
            {
                UpdateStatFromModifiers(inventoryModifiers);
            }
        }

        UpdatePointStatsModifiersFromOtherStats();

        OnStatsChange?.Invoke(this, EventArgs.Empty);
    }

    void UpdateStatFromModifiers(List<ItemManager.AddModifier> modifiers)
    {
        foreach (var modifier in modifiers)
        {
            switch (modifier.attribute)
            {
                case Attribute.Armor:
                    armor.AddModifier(modifier.value.value);
                    break;

                case Attribute.Damage:
                    minDamage.AddModifier(modifier.value.min);
                    maxDamage.AddModifier(modifier.value.max);
                    break;

                case Attribute.AttackSpeed:
                    attackSpeed.AddModifier(modifier.value.value);
                    break;

                case Attribute.Health:
                    health.AddModifier(modifier.value.value);
                    break;

                case Attribute.Mana:
                    mana.AddModifier(modifier.value.value);
                    break;
                
                case Attribute.Accuracy:
                    accuracy.AddModifier(modifier.value.value);
                    break;
                
                case Attribute.Agility: 
                    agility.AddModifier(modifier.value.value);
                    break;
                    
                case Attribute.Spirit:
                    spirit.AddModifier(modifier.value.value);
                    break;
                
                case Attribute.Strength:
                    strength.AddModifier(modifier.value.value);
                    break;
                case Attribute.HealthRegeneration:
                    healthRegeneration.AddModifier(modifier.value.value);
                    break;
                case Attribute.ManaRegeneration:
                    manaRegeneration.AddModifier(modifier.value.value);
                    break;
            }
        }
    }


    

    protected override void HealthChanged()
    {
        OnHealthChange?.Invoke(this, new OnHealthChangeArgs { health = health.currentValue, maxHealth = GetMaxHealth() });
    }

    protected override void ManaChanged()
    {
        OnManaChange?.Invoke(this, new OnManaChangeArgs { mana = mana.currentValue, maxMana = mana.GetMaxValue()  });
    }

    protected override void Die()
    {
        base.Die();

        PlayerManager.instance.KillPlayer();
    }

}
