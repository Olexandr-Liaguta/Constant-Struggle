using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickUpItemUI : MonoBehaviour
{
    [SerializeField]
    Image image;
    
    [SerializeField]
    TextMeshProUGUI description;

    [SerializeField]
    ItemNameUI itemNameUI;

    public InventoryItem pickUpItem;

    PickUpUI pickUpUI;

    private void Start()
    {
        pickUpUI = GetComponentInParent<PickUpUI>();
    }

    public void SetItem(InventoryItem inventoryItem)
    {
        pickUpItem = inventoryItem;

        image.sprite = inventoryItem.item.icon;

        itemNameUI.SetInventoryItem(inventoryItem);

        description.text = "";
    }

    public void PickUp()
    {
        pickUpUI.PickUpItem(pickUpItem, gameObject);
    }

}
