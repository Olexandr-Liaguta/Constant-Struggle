using System.Collections.Generic;

public class PickUpObject : Interactable
{
    public List<InventoryItem> items;

    public override void Interact()
    {
        base.Interact();

        PickUpManager.instance.ShowPickUp(this);
    }

    public bool PickUpItem(InventoryItem item)
    {
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
        {
            items.Remove(item);
        }

        if (items.Count == 0)
        {
            Destroy(gameObject);
        }

        return wasPickedUp;
    }


}
