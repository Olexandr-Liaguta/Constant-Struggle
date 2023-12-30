using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



enum ItemType
{
    Amulet,
    Chest,
    Foot,
    Gloves,
    Head,
    Legs,
    Ring,
    Shield,
    Weapon,
    Other,
}

public class ItemManager : MonoBehaviour
{
    #region Singletone
    static public ItemManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    List<Item> allItems;

    [Serializable]
    public class AddModifier
    {
        public Attribute attribute;
        public ModifierValue value;
    }

    readonly Dictionary<ItemType, List<Item>> items = new()
    {
        {ItemType.Amulet, new List<Item>() },
        {ItemType.Chest, new List<Item>() },
        {ItemType.Foot, new List<Item>() },
        {ItemType.Gloves, new List<Item>() },
        {ItemType.Head, new List<Item>() },
        {ItemType.Legs, new List<Item>() },
        {ItemType.Other, new List<Item>() },
        {ItemType.Ring, new List<Item>() },
        {ItemType.Shield, new List<Item>() },
        {ItemType.Weapon, new List<Item>() },
    };

    List<ResourceItem> resourceItems = new();

    ItemRarityManager itemRarityManager;

    void Start()
    {
        itemRarityManager = GetComponent<ItemRarityManager>();

        allItems = Resources.LoadAll<Item>("").ToList();

        foreach (var item in allItems)
        {
            if (item is AmuletEquipment)
            {
                items[ItemType.Amulet].Add(item);
            }
            else if (item is ChestEquipment)
            {
                items[ItemType.Chest].Add(item);
            }
            else if (item is FootEquipment)
            {
                items[ItemType.Foot].Add(item);
            }
            else if (item is GlovesEquipment)
            {
                items[ItemType.Gloves].Add(item);
            }
            else if (item is HeadEquipment)
            {
                items[ItemType.Head].Add(item);
            }
            else if (item is LegsEquipment)
            {
                items[ItemType.Legs].Add(item);
            }
            else if (item is RingEquipment)
            {
                items[ItemType.Ring].Add(item);
            }
            else if (item is ShieldEquipment)
            {
                items[ItemType.Shield].Add(item);
            }
            else if (item is WeaponEquipment)
            {
                items[ItemType.Weapon].Add(item);
            }
            else if (item is ResourceItem)
            {
                resourceItems.Add(item as ResourceItem);
            }
            else
            {
                items[ItemType.Other].Add(item);
            }
        }
    }

    public List<InventoryItem> GetRandomItems(int score)
    {
        int remainScore = UnityEngine.Random.Range((int)(score * 0.85), (int)(score * 1.15));

        List<InventoryItem> randomItems = new();

        while (remainScore > 0)
        {
            if (remainScore < ScoresHelper.itemRarityScores[ItemRarity.Simple])
            {
                HandleRandomResourse(ref remainScore, ref randomItems);
            }
            else
            {
                bool isResource = UnityEngine.Random.Range(0, 100) < 50;

                if (isResource)
                {
                    HandleRandomResourse(ref remainScore, ref randomItems);
                }
                else
                {
                    HandleRandomEquipment(ref remainScore, ref randomItems);
                }
            }
        }

        return randomItems;
    }

    void HandleRandomResourse(ref int remainScore, ref List<InventoryItem> randomItems)
    {
        InventoryItem inventoryResource = _GetRandomResource();

        ResourceType resourceType = (inventoryResource.item as ResourceItem).type;

        int resourceScore = ScoresHelper.resourceScores[resourceType];

        int randomQuantity = _GetRandomQuantity(remainScore, resourceScore);

        inventoryResource.quantity = randomQuantity;

        remainScore -= resourceScore * randomQuantity;

        int resourceInItemsIndex = randomItems
            .FindIndex(inventoryItem =>
                inventoryItem.item is ResourceItem && (inventoryItem.item as ResourceItem).type == resourceType
            );

        if (resourceInItemsIndex >= 0)
        {
            randomItems[resourceInItemsIndex].quantity += inventoryResource.quantity;
        }
        else
        {
            randomItems.Add(inventoryResource);
        }
    }

    InventoryItem _GetRandomResource()
    {
        int randomResourceIndex = UnityEngine.Random.Range(0, resourceItems.Count);
        ResourceItem randomResourceItem = resourceItems[randomResourceIndex];

        return new InventoryItem(randomResourceItem);
    }

    int _GetRandomQuantity(int remainScore, int resourceScore)
    {
        int quantityLimit = 200;

        int avaibleQuantity = Mathf.Clamp(remainScore / resourceScore, 0, quantityLimit);
        return UnityEngine.Random.Range(1, avaibleQuantity);
    }

    void HandleRandomEquipment(ref int remainScore, ref List<InventoryItem> randomItems)
    {
        ItemType randomItemType = _GetRandomItemType();

        Item randomItem = _GetRandomItem(randomItemType);

        ItemRarityRandom itemRarityRandom = itemRarityManager.GetRandom(remainScore);

        InventoryItem inventoryItem = new(randomItem);

        if (itemRarityRandom == null)
        {
            inventoryItem.rarity = ItemRarity.Simple;
            remainScore -= ScoresHelper.itemRarityScores[ItemRarity.Simple];
        }
        else
        {
            inventoryItem.addModifiers = itemRarityRandom.modifiers;
            inventoryItem.rarity = itemRarityRandom.itemRarity;
            remainScore -= itemRarityRandom.rarityScore;
        }

        randomItems.Add(inventoryItem);
    }

    ItemType _GetRandomItemType()
    {
        var itemTypeValues = Enum.GetValues(typeof(ItemType));

        int randomIndexItemType = UnityEngine.Random.Range(0, itemTypeValues.Length);

        return (ItemType)itemTypeValues.GetValue(randomIndexItemType);
    }

    Item _GetRandomItem(ItemType itemType)
    {
        List<Item> randomItemList = items[itemType];

        int randomItemListIndex = UnityEngine.Random.Range(0, randomItemList.Count);

        return randomItemList[randomItemListIndex];
    }

}
