using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpUI : MonoBehaviour
{
    [SerializeField]
    GameObject pickUpCanvas, pickUpItemsContainer, pickUpItem;

    List<GameObject> createdPickUpItems = new();

    private void Start()
    {
        PickUpManager.Instance.OnShowPickUp += ShowPickUpItems;
    }

    public void ShowPickUpItems(object sender, PickUpManager.OnShowPickUpArgs args)
    {
        GameManager.Instance.StackCameraAndShowCursor();

        InstantiatePickUpItems(args.items);

        pickUpCanvas.SetActive(true);
    }

    private void InstantiatePickUpItems(List<InventoryItem> items)
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
        bool wasPickedUp = PickUpManager.Instance.PickUpItem(itemToPickUp);

        if (wasPickedUp)
        {
            createdPickUpItems.Remove(itemGO);
            Destroy(itemGO);
        }

        if (createdPickUpItems.Count == 0)
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            PickUpAllItems();
        }
    }

    void ClosePickUpItems()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.UnstackCameraAndHideCursor();

        pickUpCanvas.SetActive(false);

        DestroyAllPickUpUIGOs();
    }

    void PickUpAllItems()
    {
        foreach (GameObject createdPickUpItem in createdPickUpItems)
        {
            var pickUpItemUI = createdPickUpItem.GetComponent<PickUpItemUI>();

            bool wasPickedUp = PickUpManager.Instance.PickUpItem(pickUpItemUI.pickUpItem);

            if (!wasPickedUp)
            {
                // TODO: Notification that can`t pick up items
                return;
            }
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
