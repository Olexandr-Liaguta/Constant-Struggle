using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    #region Singletone
    static public ItemManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    List<Item> allItems;

    List<AmuletEquipment> amuletItems = new();
    List<ChestEquipment> chestItems = new();
    List<FootEquipment> footItems = new();
    List<GlovesEquipment> glovesItems = new();
    List<HeadEquipment> headItems = new();
    List<LegsEquipment> legsItems = new();
    List<RingEquipment> ringItems = new();
    List<ShieldEquipment> shieldItems = new();
    List<WeaponEquipment> weaponItems = new();

    List<Item> otherItems = new();

    void Start()
    {
        allItems = Resources.LoadAll<Item>("").ToList();

        foreach (var item in allItems)
        {
            if (item is AmuletEquipment)
            {
                amuletItems.Add(item as AmuletEquipment);
            }
            else if (item is ChestEquipment)
            {
                chestItems.Add(item as ChestEquipment);
            }
            else if (item is FootEquipment)
            {
                footItems.Add(item as FootEquipment);
            }
            else if (item is GlovesEquipment)
            {
                glovesItems.Add(item as GlovesEquipment);
            }
            else if (item is HeadEquipment)
            {
                headItems.Add(item as HeadEquipment);
            }
            else if (item is LegsEquipment)
            {
                legsItems.Add(item as LegsEquipment);
            }
            else if (item is RingEquipment)
            {
                ringItems.Add(item as RingEquipment);
            }
            else if (item is ShieldEquipment)
            {
                shieldItems.Add(item as ShieldEquipment);
            }
            else if (item is WeaponEquipment)
            {
                weaponItems.Add(item as WeaponEquipment);
            }
            else
            {
                otherItems.Add(item);
            }
        }
    }

    public Item GetRandomItem()
    {
        int random = Random.Range(0, allItems.Count);

        return allItems[random];
    }

}
