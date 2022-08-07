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

    public List<Item> items = new();

    public int space = 20;

    public delegate void OnItemsChanged();
    public OnItemsChanged onItemsChangedCallback;

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                return false;
            }
            items.Add(item);

            if (onItemsChangedCallback != null) onItemsChangedCallback.Invoke();
        }

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemsChangedCallback != null) onItemsChangedCallback.Invoke();
    }

}
