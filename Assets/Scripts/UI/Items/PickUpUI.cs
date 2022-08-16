using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpUI : MonoBehaviour
{
    [SerializeField]
    GameObject pickUpCanvas, pickUpItemsContainer, pickUpItem;

    List<GameObject> createdObjects = new();

    public void ShowPickUpItems(List<Item> items)
    {
        GameManager.instance.StackCameraAndShowCursor();

        InstantiadePickUpItems(items);

        pickUpCanvas.SetActive(true);
    }

    private void InstantiadePickUpItems(List<Item> items)
    {
        foreach (Item item in items)
        {
            GameObject createdPickUpItem = Instantiate(pickUpItem);
            createdPickUpItem.transform.SetParent(pickUpItemsContainer.transform);

            PickUpItemUI createdPickUpItemUI = createdPickUpItem.GetComponent<PickUpItemUI>();
            createdPickUpItemUI.SetItem(item);

            createdObjects.Add(createdPickUpItem);
        }
    }

    public void PickUpItem(Item itemToPickUp, GameObject itemGO)
    {
        bool wasPickedUp = PickUpManager.instance.PickUpItem(itemToPickUp);

        if (wasPickedUp) { 
            createdObjects.Remove(itemGO);
            Destroy(itemGO);
        }

        if(createdObjects.Count == 0)
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
    }

    private void ClosePickUpItems()
    {
        GameManager.instance.UnstackCameraAndHideCursor();

        pickUpCanvas.SetActive(false);

        foreach (GameObject gameObject in createdObjects)
        {
            Destroy(gameObject);
        }

        createdObjects.Clear();
    }

}
