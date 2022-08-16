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
    TextMeshProUGUI title, description;

    Item pickUpItem;

    PickUpUI pickUpUI;

    private void Start()
    {
        pickUpUI = GetComponentInParent<PickUpUI>();
    }

    public void SetItem(Item item)
    {
        pickUpItem = item;

        image.sprite = item.icon;

        title.text = item.name;
        description.text = "";
    }

    public void PickUp()
    {
        pickUpUI.PickUpItem(pickUpItem, gameObject);
    }

}
