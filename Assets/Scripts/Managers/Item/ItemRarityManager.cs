using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Modifier
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
}

public class ItemRarityRandom
{
    public int rarityScore;
    public ItemRarity itemRarity;
    public Dictionary<Modifier, ModifierValue> modifiers = new();
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

        ItemRarityRandom itemRarityRandom = new();

        itemRarityRandom.itemRarity = rarity;

        ItemRarityModifiers itemRarityModifier = RarityHelper.itemRarityModifiersInfo[rarity];

        int randomModifiersCount = Random.Range(itemRarityModifier.fromModifiersCount, itemRarityModifier.toModifiersCount + 1);

        List<Modifier> modifiers = System.Enum.GetValues(typeof(Modifier)).Cast<Modifier>().ToList();

        int rarityScore = ScoresHelper.itemRarityScores[rarity];

        for (int i = 0; i < randomModifiersCount; i++)
        {
            int randomModifierIndex = Random.Range(0, modifiers.Count);

            Modifier randomModifier = modifiers[randomModifierIndex];

            int modifierScore = ScoresHelper.modifierScore[randomModifier];

            int availableScore = rarityScore / randomModifiersCount;

            int modifierValue = availableScore / modifierScore;

            if (randomModifier == Modifier.Damage)
            {
                itemRarityRandom.modifiers.Add(randomModifier, new ModifierValue(min: modifierValue, max: modifierValue));

            }
            else
            {
                itemRarityRandom.modifiers.Add(randomModifier, new ModifierValue(modifierValue));
            }

            itemRarityRandom.rarityScore += modifierValue * modifierScore;

            modifiers.Remove(randomModifier);
        }

        return itemRarityRandom;
    }

}
