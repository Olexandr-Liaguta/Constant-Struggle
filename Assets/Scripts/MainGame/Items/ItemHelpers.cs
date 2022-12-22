using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemHelpers
{
   static  public readonly Dictionary<ItemRarity, Color> rarityColors = new()
    {
        {ItemRarity.Simple, Color.white },
        {ItemRarity.Improved, Color.grey},
        {ItemRarity.Magical, Color.blue},
        {ItemRarity.Rare, Color.yellow},
        {ItemRarity.Demonic, Color.red},
        {ItemRarity.Set, Color.green},
    };


}
