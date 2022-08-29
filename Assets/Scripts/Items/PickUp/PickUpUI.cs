using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpUI : MonoBehaviour
{
    [SerializeField]
    GameObject pickUpCanvas, pickUpItemsContainer, pickUpItem;

    List<GameObject> createdPickUpItems = new();

    public void ShowPickUpItems(List<InventoryItem> items)
    {
        GameManager.instance.StackCameraAndShowCursor();

        InstantiadePickUpItems(items);

        pickUpCanvas.SetActive(true);
    }

    private void InstantiadePickUpItems(List<InventoryItem> items)
    {
        foreach (InventoryItem item in items)
        {
            GameObject createdPickUpItem = Instantiate(pickUpItem);
            createdPickUpItem.transform.SetParent(pickUpItemsContainer.transform);

            PickUpItemUI createdPickUpItemUI = createdPickUpItem.GetComponent<PickUpItemUI>();
            createdPickUpItemUI.SetItem(item);

            createdPickUpItems.Add(createdPickUpItem);
        }
    }

    public void PickUpItem(InventoryItem itemToPickUp, GameObject itemGO)
    {
        bool wasPickedUp = PickUpManager.instance.PickUpItem(itemToPickUp);

        if (wasPickedUp) { 
            createdPickUpItems.Remove(itemGO);
            Destroy(itemGO);
        }

        if(createdPickUpItems.Count == 0)
        {
            ClosePickUpItems();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePickUpItems();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            PickUpAllItems();
        }
    }

    void ClosePickUpItems()
    {
        GameManager.instance.UnstackCameraAndHideCursor();

        pickUpCanvas.SetActive(false);

        DestroyAllPickUpUIGOs();
    }

    void PickUpAllItems()
    {
        foreach (GameObject createdPickUpItem in createdPickUpItems)
        {
            var pickUpItemUI = createdPickUpItem.GetComponent<PickUpItemUI>();

            PickUpManager.instance.PickUpItem(pickUpItemUI.pickUpItem);
        }

        ClosePickUpItems();
    }

    void DestroyAllPickUpUIGOs()
    {
        foreach (GameObject gameObject in createdPickUpItems)
        {
            Destroy(gameObject);
        }

        createdPickUpItems.Clear();
    }
}
