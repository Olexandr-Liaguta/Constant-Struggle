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

    PickUpObject _pickUpObject;


    public void ShowPickUp(PickUpObject pickUpObject)
    {
        _pickUpObject = pickUpObject;

        pickUpUI.ShowPickUpItems(_pickUpObject.items);
    }

    public bool PickUpItem(Item item)
    {
        return _pickUpObject.PickUpItem(item);
    }

}
