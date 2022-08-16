using System.Collections.Generic;

public class PickUpObject : Interactable
{
    public List<Item> items;

    public override void Interact()
    {
        base.Interact();

        PickUpManager.instance.ShowPickUp(this);
    }

    public bool PickUpItem(Item item)
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
