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

    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChange;
    }

    void OnEquipmentChange(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            minDamage.AddModifier(newItem.minDamageModifier);
            maxDamage.AddModifier(newItem.maxDamageModifier);
        }

        Debug.Log("Old item min: " + oldItem.minDamageModifier);

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            minDamage.RemoveModifier(oldItem.minDamageModifier);
            maxDamage.RemoveModifier(oldItem.maxDamageModifier);
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
