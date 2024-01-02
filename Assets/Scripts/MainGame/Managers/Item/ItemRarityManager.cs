using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Attribute
{
    Damage,
    AttackSpeed,

    Armor,

    Health,
    Mana,

    Strength,
    Spirit,
    Accuracy,
    Agility,

    HealthRegeneration,
    ManaRegeneration,
}

public class ItemRarityRandom
{
    public int rarityScore;
    public ItemRarity itemRarity;
    public List<ItemManager.AddModifier> modifiers = new();
}

public class ItemRarityManager : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;

    ItemRarity[] itemRaritiesForHanlde = new ItemRarity[5]
    {
        ItemRarity.Set,
        ItemRarity.Demonic,
        ItemRarity.Rare,
        ItemRarity.Magical,
        ItemRarity.Improved,
    };
    public ItemRarityRandom? GetRandom(int score)
    {
        List<ItemRarity> availableItemRarities = RarityHelper.GetAvailableEquipmentRarity(score);

        if (availableItemRarities.Count == 0)
        {
            return null;
        }

        ItemRarity itemRarity = HandleItemRarity(availableItemRarities);

        ItemRarityRandom itemRarityRandom = HandleModifiers(itemRarity);

        return itemRarityRandom;
    }

    ItemRarity HandleItemRarity(List<ItemRarity> availableItemRarities)
    {
        float random = Random.Range(0f, 100f);

        foreach (ItemRarity rarity in itemRaritiesForHanlde)
        {
            if (availableItemRarities.Contains(rarity))
            {
                float chance = RarityHelper.rarityChance[rarity];
                if (random < chance)
                {
                    return rarity;
                }
            }
        }

        return ItemRarity.Simple;
    }

    ItemRarityRandom HandleModifiers(ItemRarity rarity)
    {
        if (rarity == ItemRarity.Simple) return null;

        ItemRarityRandom itemRarityRandom = new()
        {
            itemRarity = rarity
        };

        ItemRarityModifiers itemRarityModifier = RarityHelper.itemRarityModifiersInfo[rarity];

        int randomModifiersCount = Random.Range(itemRarityModifier.fromModifiersCount, itemRarityModifier.toModifiersCount + 1);

        List<Attribute> allAttributes = System.Enum.GetValues(typeof(Attribute)).Cast<Attribute>().ToList();

        int rarityScore = ScoresHelper.itemRarityScores[rarity];

        for (int i = 0; i < randomModifiersCount; i++)
        {
            int randomAttributeIndex = Random.Range(0, allAttributes.Count);

            Attribute randomAttribute = allAttributes[randomAttributeIndex];

            int attributeScore = ScoresHelper.attributeScore[randomAttribute];

            int availableScore = rarityScore / randomModifiersCount;

            int modifierValue = availableScore / attributeScore;

            if (randomAttribute == Attribute.Damage)
            {
                itemRarityRandom.modifiers.Add(
                    new ItemManager.AddModifier()
                    {
                        attribute = randomAttribute,
                        value = new ModifierValue(min: modifierValue, max: modifierValue)
                    }
               );
            }
            else
            {
                itemRarityRandom.modifiers.Add(
                    new ItemManager.AddModifier()
                    {
                        attribute = randomAttribute,
                        value = new ModifierValue(modifierValue)
                    }
               );
            }

            itemRarityRandom.rarityScore += modifierValue * attributeScore;

            allAttributes.Remove(randomAttribute);
        }

        return itemRarityRandom;
    }

}
