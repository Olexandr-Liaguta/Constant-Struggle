using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI_GO;

    [SerializeField] private GameObject inventorySlotUIPrefab;
    [SerializeField] Transform resourcesParent;


    List<GameObject> instantiatedItemSlots = new();

    InventorySlotUI[] itemSlots;
    Guid selectedInventoryId;

    InventoryResourceUI[] resourceSlots;

    InventoryWeightUI inventoryWeightUI;

    void Start()
    {
        PlayerInventoryManager.Instance.OnItemsChanged += PlayerInventoryManager_OnItemsChanged;

        inventoryWeightUI = GetComponent<InventoryWeightUI>();

        resourceSlots = resourcesParent.GetComponentsInChildren<InventoryResourceUI>();

        UpdateUI();

        Cursor.visible = false;
        inventoryUI_GO.SetActive(false);
    }

    private void PlayerInventoryManager_OnItemsChanged(object sender, EventArgs e)
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (inventoryUI_GO.activeSelf)
            {
                inventoryUI_GO.SetActive(false);
                GameManager.Instance.UnstackCameraAndHideCursor();
            }
            else
            {
                inventoryUI_GO.SetActive(true);
                GameManager.Instance.StackCameraAndShowCursor();
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
        DestroyAllInventaryGameObjects();

        var inventoryItems = PlayerInventoryData.GetInventoryItems();

        foreach (InventoryItem inventorySlot in inventoryItems)
        {
            GameObject instantiatedInventoryItem = Instantiate(inventorySlotUIPrefab);

            instantiatedInventoryItem.transform.SetParent(itemsParent.transform);

            var inventorySlotUI = instantiatedInventoryItem.GetComponent<InventorySlotUI>();
            inventorySlotUI.SetItem(inventorySlot);

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
