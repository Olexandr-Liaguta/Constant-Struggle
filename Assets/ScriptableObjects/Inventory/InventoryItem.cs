using System;
using System.Collections.Generic;

public enum ItemRarity
{
    Simple,
    Improved,
    Magical,
    Rare,
    Demonic,
    Set
}

[Serializable]
public class InventoryItem
{
    public readonly Item item;
    public int quantity;
    public readonly Guid id;

    public ItemRarity rarity;
    public Dictionary<Modifier, ModifierValue> addModifiers = new();

    public InventoryItem(Item item)
    {
        this.item = item;
        id = Guid.NewGuid();
    }

    public void Use()
    {
        if (item is Equipment)
        {
            EquipmentSlotExact? equipmentSlotExact = EquipmentManager.instance.GetEquipmentSlot(this);

            if (equipmentSlotExact == null)
            {
                EquipmentManager.instance.Equip(this);
                RemoveFromInventory();
            }
            else
            {
                EquipmentManager.instance.Unequip((EquipmentSlotExact)equipmentSlotExact);
            }
        }
    }

    void RemoveFromInventory()
    {
        PlayerInventoryManager.instance.Remove(this);
    }
}
