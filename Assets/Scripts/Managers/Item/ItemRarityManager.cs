using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Modifier
{
    MinDamage,
    MaxDamage,
    AttackSpeed,
    Armor,
    Health,
    Mana,
}

class ItemRarityModifiers
{
    public int fromModifiersCount;
    public int toModifiersCount;

    public ItemRarityModifiers(int from, int to)
    {
        fromModifiersCount = from;
        toModifiersCount = to;
    }
}

public class ItemRarityRandom
{
    public int rarityScore;
    public ItemRarity itemRarity;
    public Dictionary<Modifier, int> modifiers = new();
}

public class ItemRarityManager : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;

    Dictionary<ItemRarity, ItemRarityModifiers> itemRarityModifiersInfo = new()
    {
        {ItemRarity.Improved, new ItemRarityModifiers(1, 2) },
        {ItemRarity.Magical, new ItemRarityModifiers(1, 3) },
        {ItemRarity.Rare, new ItemRarityModifiers(3, 5) },
        {ItemRarity.Demonic, new ItemRarityModifiers(4, 6) },
        {ItemRarity.Set, new ItemRarityModifiers(4, 6) },
    };

    public ItemRarityRandom? GetRandom(int score)
    {
        List<ItemRarity> itemRarities = ItemScores.GetAvailableEquipmentRarity(score);

        if (itemRarities.Count == 0)
        {
            return null;
        }

        ItemRarity itemRarity = HandleItemRarity();

        ItemRarityRandom itemRarityRandom = HandleModifiers(itemRarity);

        return itemRarityRandom;
    }

    ItemRarity HandleItemRarity()
    {
        float random = Random.Range(0f, 100f);

        float chance = playerStats.rarityChance[ItemRarity.Set];
        if (random < chance)
        {
            return ItemRarity.Set;
        }

        chance = playerStats.rarityChance[ItemRarity.Demonic];
        if (random < chance)
        {
            return ItemRarity.Demonic;
        }

        chance = playerStats.rarityChance[ItemRarity.Rare];
        if (random < chance)
        {
            return ItemRarity.Rare;
        }

        chance = playerStats.rarityChance[ItemRarity.Magical];
        if (random < chance)
        {
            return ItemRarity.Magical;
        }

        chance = playerStats.rarityChance[ItemRarity.Improved];
        if (random < chance)
        {
            return ItemRarity.Improved;
        }

        return ItemRarity.Simple;
    }

    ItemRarityRandom HandleModifiers(ItemRarity rarity)
    {
        if (rarity == ItemRarity.Simple) return null;

        ItemRarityRandom itemRarityRandom = new();

        itemRarityRandom.itemRarity = rarity;

        ItemRarityModifiers itemRarityModifier = itemRarityModifiersInfo[rarity];

        int randomModifiersCount = Random.Range(itemRarityModifier.fromModifiersCount, itemRarityModifier.toModifiersCount + 1);

        List<Modifier> modifiers = System.Enum.GetValues(typeof(Modifier)).Cast<Modifier>().ToList();

        int rarityScore = ItemScores.itemRarityScores[rarity];

        for (int i = 0; i < randomModifiersCount; i++)
        {
            int randomModifierIndex = Random.Range(0, modifiers.Count);
            Modifier randomModifier = modifiers[randomModifierIndex];

            int modifierScore = ItemScores.modifierScore[randomModifier];

            int availableScore = rarityScore / randomModifiersCount;

            int modifierValue = availableScore / modifierScore;

            itemRarityRandom.modifiers.Add(randomModifier, modifierValue);

            itemRarityRandom.rarityScore += modifierValue * modifierScore;

            modifiers.Remove(randomModifier);
        }

        return itemRarityRandom;
    }

}
