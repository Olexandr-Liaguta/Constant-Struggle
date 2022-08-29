using System.Collections.Generic;
using System.Linq;

public abstract class ItemScores
{
    static public readonly Dictionary<ItemRarity, int> itemRarityScores = new()
    {
        {ItemRarity.Simple, 10 },
        {ItemRarity.Improved, 50 },
        {ItemRarity.Magical, 100 },
        {ItemRarity.Rare, 200 },
        {ItemRarity.Demonic, 400 },
        {ItemRarity.Set, 600 },
    };

    static public List<ItemRarity> GetAvailableEquipmentRarity(int score) 
    {
        List<ItemRarity> itemRarities = new(itemRarityScores.Count);

        foreach(var itemRarityScore in itemRarityScores)
        {
            if(score >= itemRarityScore.Value && itemRarityScore.Key != ItemRarity.Simple)
            {
                itemRarities.Add(itemRarityScore.Key);
            }
        }

        return itemRarities;
    }

    static public readonly Dictionary<ResourceType, int> resourceScores = new()
    {
        { ResourceType.Crystal, 6 },
        { ResourceType.Food, 3 },
        { ResourceType.Steel, 2 },
        { ResourceType.Wood, 1 },
    };

    static public readonly Dictionary<Modifier, int> modifierScore = new()
    {
        { Modifier.Armor, 1 },
        { Modifier.AttackSpeed, 6 },
        { Modifier.Health, 1 },
        { Modifier.Mana, 1 },
        { Modifier.MaxDamage, 1 },
        { Modifier.MinDamage, 2 },
    };

}
