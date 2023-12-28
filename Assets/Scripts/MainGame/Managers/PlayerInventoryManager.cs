using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    #region Singelton

    public static PlayerInventoryManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    [SerializeField] private InventorySO playerInventory;

    public delegate void OnItemsChanged();
    public OnItemsChanged onItemsChangedCallback;

    public bool Add(InventoryItem inventoryItem)
    {
        bool isApplied = playerInventory.Add(inventoryItem);

        if (onItemsChangedCallback != null) onItemsChangedCallback.Invoke();

        return isApplied;
    }

    public void Remove(InventoryItem item)
    {
        playerInventory.Remove(item);

        if (onItemsChangedCallback != null) onItemsChangedCallback.Invoke();
    }

    public float GetCurrentWeight()
    {
        return playerInventory.GetCurrentWeight();
    }

    public List<InventoryItem> GetInventoryItems()
    {
        return playerInventory.items;
    }

    public Dictionary<ResourceType, int> GetResourses()
    {
        return playerInventory.resources;
    }
}
