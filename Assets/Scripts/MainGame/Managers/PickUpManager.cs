using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{

    #region Singelton

    static public PickUpManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    [SerializeField]
    PickUpUI pickUpUI;

    [SerializeField]
    GameObject pickUpGO;

    PickUpObject _pickUpObject;


    public void ShowPickUp(PickUpObject pickUpObject)
    {
        _pickUpObject = pickUpObject;

        pickUpUI.ShowPickUpItems(_pickUpObject.items);
    }

    public bool PickUpItem(InventoryItem item)
    {
        return _pickUpObject.PickUpItem(item);
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
