using System.Collections.Generic;

public abstract class RarityHelper
{
    static public readonly Dictionary<ItemRarity, float> rarityChance = new()
    {
        {ItemRarity.Improved, 50},
        {ItemRarity.Magical, 30},
        {ItemRarity.Rare, 5},
        {ItemRarity.Demonic, 1},
        {ItemRarity.Set, 0.1f},
    };

    static public List<ItemRarity> GetAvailableEquipmentRarity(int score) 
    {
        List<ItemRarity> itemRarities = new(ScoresHelper.itemRarityScores.Count);

        foreach(var itemRarityScore in ScoresHelper.itemRarityScores)
        {
            if(score >= itemRarityScore.Value && itemRarityScore.Key != ItemRarity.Simple)
            {
                itemRarities.Add(itemRarityScore.Key);
            }
        }

        return itemRarities;
    }

    static public readonly Dictionary<ItemRarity, ItemRarityModifiers> itemRarityModifiersInfo = new()
    {
        {ItemRarity.Improved, new ItemRarityModifiers(1, 2) },
        {ItemRarity.Magical, new ItemRarityModifiers(1, 3) },
        {ItemRarity.Rare, new ItemRarityModifiers(3, 5) },
        {ItemRarity.Demonic, new ItemRarityModifiers(4, 6) },
        {ItemRarity.Set, new ItemRarityModifiers(4, 6) },
    };

}

public class ItemRarityModifiers
{
    public int fromModifiersCount;
    public int toModifiersCount;

    public ItemRarityModifiers(int from, int to)
    {
        fromModifiersCount = from;
        toModifiersCount = to;
    }
}