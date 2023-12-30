using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public static class PlayerInventoryData
{
    private static List<InventoryItem> items = new();

    [Serializable]
    public class Resource
    {
        public ResourceType type;
        public int value;
    }

    private static List<Resource> resources = new()
    {
        new Resource { type = ResourceType.Steel, value=0},
        new Resource { type = ResourceType.Crystal, value=0},
        new Resource { type = ResourceType.Food, value=0},
        new Resource { type = ResourceType.Wood, value=0},
    };



    [Serializable]
    public class EquipmentSlot
    {
        public EquipmentSlotExact slot;
        public InventoryItem inventoryItem;
    }

    private static List<EquipmentSlot> equipments = new() {
        new EquipmentSlot { slot = EquipmentSlotExact.Ring_1, inventoryItem = null},
        new EquipmentSlot { slot = EquipmentSlotExact.Ring_2, inventoryItem = null},
        new EquipmentSlot { slot = EquipmentSlotExact.Feet, inventoryItem = null},
        new EquipmentSlot { slot = EquipmentSlotExact.Amulet, inventoryItem = null},
        new EquipmentSlot { slot = EquipmentSlotExact.Chest, inventoryItem = null},
        new EquipmentSlot { slot = EquipmentSlotExact.Gloves, inventoryItem = null},
        new EquipmentSlot { slot = EquipmentSlotExact.Hand_1, inventoryItem = null},
        new EquipmentSlot { slot = EquipmentSlotExact.Hand_2, inventoryItem = null},
        new EquipmentSlot { slot = EquipmentSlotExact.Head, inventoryItem = null},
        new EquipmentSlot { slot = EquipmentSlotExact.Legs, inventoryItem = null},
    };



    public static void SetEmptyInventory()
    {
        items = new();

        resources = new()
        {
            new Resource { type = ResourceType.Steel, value=0},
            new Resource { type = ResourceType.Crystal, value=0},
            new Resource { type = ResourceType.Food, value=0},
            new Resource { type = ResourceType.Wood, value=0},
        };

        equipments = new() {
            new EquipmentSlot { slot = EquipmentSlotExact.Ring_1, inventoryItem = null},
            new EquipmentSlot { slot = EquipmentSlotExact.Ring_2, inventoryItem = null},
            new EquipmentSlot { slot = EquipmentSlotExact.Feet, inventoryItem = null},
            new EquipmentSlot { slot = EquipmentSlotExact.Amulet, inventoryItem = null},
            new EquipmentSlot { slot = EquipmentSlotExact.Chest, inventoryItem = null},
            new EquipmentSlot { slot = EquipmentSlotExact.Gloves, inventoryItem = null},
            new EquipmentSlot { slot = EquipmentSlotExact.Hand_1, inventoryItem = null},
            new EquipmentSlot { slot = EquipmentSlotExact.Hand_2, inventoryItem = null},
            new EquipmentSlot { slot = EquipmentSlotExact.Head, inventoryItem = null},
            new EquipmentSlot { slot = EquipmentSlotExact.Legs, inventoryItem = null},
        };
    }


    public static bool AddInventoryItem(InventoryItem inventoryItem)
    {
        if (inventoryItem.item is ResourceItem resourceItem)
        {
            int resourceIndex = resources.FindIndex(resource => resource.type == resourceItem.type);

            resources[resourceIndex].value += inventoryItem.quantity;

            return true;
        }

        if (!inventoryItem.item.isDefaultItem)
        {
            items.Add(inventoryItem);
        }

        return true;
    }

    public static void RemoveInventoryItem(InventoryItem item)
    {
        items.Remove(item);
    }

    public static List<InventoryItem> GetInventoryItems()
    {
        return items;
    }

    public static void SetInventoryItems(List<InventoryItem> newItems)
    {
        items = newItems;
    }

    public static void SetEquipmentSlot(EquipmentSlotExact slot, InventoryItem inventoryItem)
    {
        int equipmentIndex = equipments.FindIndex(equipment => equipment.slot == slot);

        equipments[equipmentIndex].inventoryItem = inventoryItem;
    }

    public static InventoryItem GetInventoryItemFromEquipment(EquipmentSlotExact slot)
    {
        return equipments.Find(equipment => equipment.slot == slot)?.inventoryItem;
    }

    public static EquipmentSlotExact? GetEquipmentSlotExact(InventoryItem inventoryItem)
    {
        foreach (var equipment in equipments)
        {
            if (equipment.inventoryItem?.id == inventoryItem.id)
            {
                return equipment.slot;
            }
        }

        return null;
    }

    public static List<EquipmentSlot> GetEquipments()
    {
        return equipments;
    }

    public static void SetEquipments(List<EquipmentSlot> newEquipments)
    {
        equipments = newEquipments;
    }

    public static Resource GetResource(ResourceType type)
    {
        return resources.Find(resource => resource.type == type);
    }

    public static List<Resource> GetResources()
    {
        return resources;
    }

    public static void SetResources(List<Resource> newResources)
    {
        resources = newResources;
    }

    public static float GetCurrentWeight()
    {
        float sum = 0f;

        foreach (InventoryItem inventoryItem in items)
        {
            sum += inventoryItem.item.weight;
        }

        return sum;
    }

}
