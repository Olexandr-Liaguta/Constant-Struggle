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

    public Item item { get; private set; }
    public int quantity;
    public string id { get; private set; }

    public ItemRarity rarity;
    public List<ItemManager.AddModifier> addModifiers = new();

    public InventoryItem(Item item)
    {
        this.item = item;
        id = Guid.NewGuid().ToString();
    }

    public InventoryItem(Item item, int quantity, string id, ItemRarity rarity, List<ItemManager.AddModifier> addModifiers)
    {
        this.item = item;
        this.quantity = quantity;
        this.id = id;
        this.rarity = rarity;
        this.addModifiers = addModifiers;
    }

    public void Use()
    {
        if (item is Equipment)
        {
            EquipmentSlotExact? equipmentSlotExact = PlayerInventoryData.GetEquipmentSlotExact(this);

            if (!equipmentSlotExact.HasValue)
            {
                EquipmentManager.Instance.Equip(this);
                RemoveFromInventory();
            }
            else
            {
                EquipmentManager.Instance.Unequip((EquipmentSlotExact)equipmentSlotExact);
            }
        }
    }

    void RemoveFromInventory()
    {
        PlayerInventoryManager.Instance.Remove(this);
    }
}
