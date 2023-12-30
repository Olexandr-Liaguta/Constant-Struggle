using System.Collections.Generic;
using System.Linq;

public abstract class ScoresHelper
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

    static public readonly Dictionary<ResourceType, int> resourceScores = new()
    {
        { ResourceType.Crystal, 6 },
        { ResourceType.Food, 3 },
        { ResourceType.Steel, 2 },
        { ResourceType.Wood, 1 },
    };

    static public readonly Dictionary<Attribute, int> modifierScore = new()
    {
        { Attribute.Armor, 1 },
        { Attribute.AttackSpeed, 6 },
        { Attribute.Health, 1 },
        { Attribute.Mana, 1 },
        { Attribute.Damage, 2 },
        { Attribute.Accuracy, 3 },
        { Attribute.Agility, 3 },
        { Attribute.Spirit, 3 },
        { Attribute.Strength, 3 },
        { Attribute.HealthRegeneration, 6 },
        { Attribute.ManaRegeneration, 3 },
    };

}
