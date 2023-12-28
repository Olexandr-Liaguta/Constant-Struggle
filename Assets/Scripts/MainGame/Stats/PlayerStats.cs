using System;
using System.Collections;
using System.Collections.Generic;

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


    private void Awake()
    {
        Instance = this;
    }

    new void Start()
    {
        base.Start();

        EquipmentManager.instance.OnEquipmentChanged += ChangeEquipment;

        OnHealthChange?.Invoke(this, new OnHealthChangeArgs { health = health.currentValue, maxHealth = GetMaxHealth() });
        OnManaChange?.Invoke(this, new OnManaChangeArgs { mana = mana.currentValue, maxMana = mana.GetMaxValue() });
    }

    void ChangeEquipment(object sender, EquipmentManager.OnEquipmentChangedArgs args)
    {
        if (args.oldItem != null)
        {
            _RemoveModifiers(args.oldItem);
        }

        if (args.newItem != null)
        {
            _AddModifiers(args.newItem);
        }

        _UpdatePointStats();

        HealthChanged();
        ManaChanged();
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
                case Modifier.HealthRegeneration:
                    healthRegeneration.AddModifier(modifier.Value.value);
                    break;
                case Modifier.ManaRegeneration:
                    manaRegeneration.AddModifier(modifier.Value.value);
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
                case Modifier.HealthRegeneration:
                    healthRegeneration.RemoveModifier(modifier.Value.value);
                    break;
                case Modifier.ManaRegeneration:
                    manaRegeneration.RemoveModifier(modifier.Value.value);
                    break;
            }
        }
    }

    void _UpdatePointStats()
    {
        health.SetStatModifier(strength.GetValue());
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
