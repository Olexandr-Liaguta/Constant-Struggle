using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singelton

    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    public List<InventoryItem> items = new();

    public Dictionary<ResourceType, int> resources = new()
    {
        { ResourceType.Crystal, 0 },
        { ResourceType.Food, 0 },
        { ResourceType.Steel, 0 },
        { ResourceType.Wood, 0 }
    };

    public delegate void OnItemsChanged();
    public OnItemsChanged onItemsChangedCallback;

    public bool Add(InventoryItem inventoryItem)
    {
        if (inventoryItem.item is ResourceItem)
        {
            resources[(inventoryItem.item as ResourceItem).type] += inventoryItem.quantity;

            if (onItemsChangedCallback != null) onItemsChangedCallback.Invoke();

            return true;
        }

        if (!inventoryItem.item.isDefaultItem)
        {
            items.Add(inventoryItem);

            if (onItemsChangedCallback != null) onItemsChangedCallback.Invoke();
        }

        return true;
    }

    public void Remove(InventoryItem item)
    {
        items.Remove(item);

        if (onItemsChangedCallback != null) onItemsChangedCallback.Invoke();
    }

    public float GetCurrentWeight()
    {
        float sum = 0f;

        foreach(InventoryItem inventoryItem in items)
        {
            sum += inventoryItem.item.weight;
        }

        return sum;
    }

}
