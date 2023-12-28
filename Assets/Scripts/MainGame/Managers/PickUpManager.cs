using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    static public PickUpManager Instance { get; private set; }

    public event EventHandler<OnShowPickUpArgs> OnShowPickUp;
    public class OnShowPickUpArgs: EventArgs
    {
        public List<InventoryItem> items;
    }


    [SerializeField] private GameObject pickUpGO;


    private PickUpObject pickUpObject;

    private void Awake()
    {
      Instance = this;
    }

    public void ShowPickUp(PickUpObject pickUpObject)
    {
        this.pickUpObject = pickUpObject;

        OnShowPickUp?.Invoke(this, new OnShowPickUpArgs { items = pickUpObject.items });
    }

    public bool PickUpItem(InventoryItem item)
    {
        return pickUpObject.PickUpItem(item);
    }

    public void DropPickup(GameObject gameObject, int score)
    {
        GameObject createdPickUpGO = Instantiate(pickUpGO);

        PickUpObject createdPickUpObject = createdPickUpGO.GetComponent<PickUpObject>();
        createdPickUpObject.transform.position = gameObject.transform.position;

        List<InventoryItem> items = ItemManager.instance.GetRandomItems(score);

        createdPickUpObject.items = items;
    }

}
