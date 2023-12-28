using System.Collections.Generic;

public class PickUpObject : Interactable
{
    public List<InventoryItem> items;

    public override void Interact()
    {
        base.Interact();

        PickUpManager.Instance.ShowPickUp(this);
    }

    public bool PickUpItem(InventoryItem item)
    {
        bool wasPickedUp = PlayerInventoryManager.instance.Add(item);

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
