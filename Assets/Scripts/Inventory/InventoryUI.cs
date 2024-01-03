using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform itemsParent;
    [SerializeField] private GameObject inventorySlotUIPrefab;
    [SerializeField] private Transform resourcesParent;


    private List<GameObject> instantiatedItemSlots = new();

    private InventorySlotUI[] itemSlots;
    private Guid selectedInventoryId;

    private InventoryResourceUI[] resourceSlots;

    private InventoryWeightUI inventoryWeightUI;

    void Start()
    {
        PlayerInventoryManager.Instance.OnItemsChanged += PlayerInventoryManager_OnItemsChanged;

        inventoryWeightUI = GetComponent<InventoryWeightUI>();

        resourceSlots = resourcesParent.GetComponentsInChildren<InventoryResourceUI>();

        UpdateUI();

        Cursor.visible = false;
        gameObject.SetActive(false);
    }

    private void PlayerInventoryManager_OnItemsChanged(object sender, EventArgs e)
    {
        UpdateUI();
    }

    public void SelectInventoryItem(Guid id)
    {
        selectedInventoryId = id;
        UpdateUI();
    }

    void UpdateUI()
    {
        UpdateItemsUI();

        UpdateResourcesUI();
    }

    void UpdateItemsUI()
    {
        DestroyAllInventaryGameObjects();

        var inventoryItems = PlayerInventoryData.GetInventoryItems();

        foreach (InventoryItem inventoryItem in inventoryItems)
        {
            GameObject instantiatedInventoryItem = Instantiate(inventorySlotUIPrefab);

            instantiatedInventoryItem.transform.SetParent(itemsParent.transform);

            if (inventoryItem != null)
            {
                var inventorySlotUI = instantiatedInventoryItem.GetComponent<InventorySlotUI>();
                inventorySlotUI.SetItem(inventoryItem);
            }

            instantiatedItemSlots.Add(instantiatedInventoryItem);

        }

        itemSlots = itemsParent.GetComponentsInChildren<InventorySlotUI>();

        bool isIdEqual;

        for (int i = 0; i < itemSlots.Length; i++)
        {
            isIdEqual = itemSlots[i].id.Equals(selectedInventoryId);

            itemSlots[i].SetSelectedState(isIdEqual);
        }

        inventoryWeightUI.UpdateWeightValues();
    }

    void UpdateResourcesUI()
    {
        foreach (var resourceSlot in resourceSlots)
        {
            resourceSlot.SetResourceValue(
                PlayerInventoryData.GetResource(resourceSlot.type).value
            );
        }
    }

    void DestroyAllInventaryGameObjects()
    {
        foreach (GameObject gameObject in instantiatedItemSlots)
        {
            Destroy(gameObject);
        }
    }

}
