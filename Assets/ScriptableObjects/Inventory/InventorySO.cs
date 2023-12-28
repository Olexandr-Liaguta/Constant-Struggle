using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Inventory", menuName = "States/Inventory")]
public class InventorySO : ScriptableObject
{
    public List<InventoryItem> items = new();

    [SerializeField]
    public Dictionary<ResourceType, int> resources = new()
    {
        { ResourceType.Crystal, 0 },
        { ResourceType.Food, 0 },
        { ResourceType.Steel, 0 },
        { ResourceType.Wood, 0 }
    };

    public bool Add(InventoryItem inventoryItem)
    {
        if (inventoryItem.item is ResourceItem)
        {
            resources[(inventoryItem.item as ResourceItem).type] += inventoryItem.quantity;

            return true;
        }

        if (!inventoryItem.item.isDefaultItem)
        {
            items.Add(inventoryItem);
        }

        return true;
    }

    public void Remove(InventoryItem item)
    {
        items.Remove(item);
    }

    public float GetCurrentWeight()
    {
        float sum = 0f;

        foreach (InventoryItem inventoryItem in items)
        {
            sum += inventoryItem.item.weight;
        }

        return sum;
    }

}
