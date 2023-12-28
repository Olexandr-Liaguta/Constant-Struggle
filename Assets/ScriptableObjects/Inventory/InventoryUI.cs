using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI_GO;

    [SerializeField] private GameObject inventorySlotUIPrefab;

    readonly Dictionary<Guid, GameObject> instantiatedItemSlots = new();

    InventorySlotUI[] itemSlots;
    Guid selectedInventoryId;

    [SerializeField] Transform resourcesParent;
    InventoryResourceUI[] resourceSlots;

    InventoryWeightUI inventoryWeightUI;

    void Start()
    {
        PlayerInventoryManager.instance.onItemsChangedCallback += UpdateUI;

        inventoryWeightUI = GetComponent<InventoryWeightUI>();

        resourceSlots = resourcesParent.GetComponentsInChildren<InventoryResourceUI>();

        UpdateUI();

        Cursor.visible = false;
        inventoryUI_GO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (inventoryUI_GO.activeSelf)
            {
                inventoryUI_GO.SetActive(false);
                GameManager.instance.UnstackCameraAndHideCursor();
            }
            else
            {
                inventoryUI_GO.SetActive(true);
                GameManager.instance.StackCameraAndShowCursor();
            }

        }
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
        var itemGOs = new Dictionary<Guid, GameObject>(instantiatedItemSlots);

        foreach (InventoryItem inventorySlot in PlayerInventoryManager.instance.GetInventoryItems())
        {
            itemGOs.TryGetValue(inventorySlot.id, out GameObject GO);

            if(GO == null)
            {
                GameObject instantiatedInventoryItem = Instantiate(inventorySlotUIPrefab);

                instantiatedInventoryItem.transform.SetParent(itemsParent.transform);

                var inventorySlotUI = instantiatedInventoryItem.GetComponent<InventorySlotUI>();

                inventorySlotUI.AddItem(inventorySlot);

                instantiatedItemSlots.Add(inventorySlot.id, instantiatedInventoryItem);
            } else
            {
                itemGOs.Remove(inventorySlot.id);
            }
        }

        foreach (var keyValuePair in itemGOs)
        {
            Destroy(keyValuePair.Value);
            instantiatedItemSlots.Remove(keyValuePair.Key);
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
        var resources = PlayerInventoryManager.instance.GetResourses();

        for (int i = 0; i < resourceSlots.Length; i++)
        {
            int resourceValue = resources[resourceSlots[i].type];
            resourceSlots[i].SetResourceValue(resourceValue);
        }
    }

}
