using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    public static PlayerInventoryManager Instance;

    public event EventHandler OnItemsChanged;


    [SerializeField] private InventoryUI inventoryUI;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SaveManager.Instance.OnLoadGame += SaveManager_OnLoadGame;
    }

    private void SaveManager_OnLoadGame(object sender, EventArgs e)
    {
        OnItemsChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool Add(InventoryItem inventoryItem)
    {
        bool isApplied = PlayerInventoryData.AddInventoryItem(inventoryItem);

        OnItemsChanged?.Invoke(this, EventArgs.Empty);

        return isApplied;
    }

    public void Remove(InventoryItem item)
    {
        PlayerInventoryData.RemoveInventoryItem(item);

        OnItemsChanged?.Invoke(this, EventArgs.Empty);
    }



    public void ToogleInventory()
    {
        if (inventoryUI.gameObject.activeSelf)
        {
            HideInventory();
        }
        else
        {
            ShowInventory();
        }
    }

    public void ShowInventory()
    {
        inventoryUI.gameObject.SetActive(true);
        GameManager.Instance.StackCameraAndShowCursor();
    }

    public void HideInventory()
    {
        inventoryUI.gameObject.SetActive(false);
        GameManager.Instance.UnstackCameraAndHideCursor();
        TooltipManager.Instance.Hide();
    }

    public bool IsActive()
    {
        return inventoryUI.gameObject.activeSelf;
    }

    internal void SelectInventoryItem(Guid id)
    {
        inventoryUI.SelectInventoryItem(id);
    }
}
