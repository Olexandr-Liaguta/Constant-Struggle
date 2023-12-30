using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [Serializable]
    private class GameData
    {
        public List<GameMapData.TextureList> locationRows;
        public List<InventoryItemDTO> inventoryItems;
        public List<PlayerInventoryData.Resource> resources;
        public List<EquipmentSlotDTO> equipments;
    }

    [Serializable]
    public struct InventoryItemDTO
    {
        public Item item;
        public int quantity;
        public string id;

        public ItemRarity rarity;
        public List<ItemManager.AddModifier> addModifiers;
    }

    [Serializable]
    public struct EquipmentSlotDTO
    {
        public EquipmentSlotExact slot;
        public InventoryItemDTO inventoryItemDTO;
    }



    public static SaveManager Instance { get; private set; }




    public event EventHandler OnLoadGame;


    [SerializeField] private bool isAutosave = true;

    private const string SAVE_FOLDER = "Saves";

    private int saveTime = 5;
    private float timer = 0f;


    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    void Update()
    {
        if (isAutosave)
        {
            timer += Time.deltaTime;

            if (timer > saveTime)
            {
                Debug.Log("Save");
                Save();
                timer = 0f;
            }
        }
    }

    public void Save()
    {
        var inventoryItemDTOs = MapInventoryItemsToDTO(PlayerInventoryData.GetInventoryItems());
        var equipmentsDTOs = MapEquipmentsToDTOs(PlayerInventoryData.GetEquipments());

        List<GameMapData.TextureList> locationRows = new();

        foreach (var locationRow in GameMapData.locationRows)
        {
            locationRows.Add(locationRow);
        }

        GameData gameData = new()
        {
            locationRows = locationRows,
            inventoryItems = inventoryItemDTOs,
            equipments = equipmentsDTOs,
            resources = PlayerInventoryData.GetResources(),
        };
        string jsonString = JsonUtility.ToJson(gameData, true);

        File.WriteAllText(SAVE_FOLDER + "/save.txt", jsonString);
    }

    public void Load()
    {
        try
        {
            string savedString = File.ReadAllText(SAVE_FOLDER + "/save.txt");
            GameData gameData = JsonUtility.FromJson<GameData>(savedString);

            GameMapData.SetLocationRows(gameData.locationRows);

            var inventoryItems = MapInventoryItemsFromDTOs(gameData.inventoryItems);
            var equipments = MapEquipmentsFromDTOs(gameData.equipments);

            PlayerInventoryData.SetInventoryItems(inventoryItems);
            PlayerInventoryData.SetResources(gameData.resources);
            PlayerInventoryData.SetEquipments(equipments);

            Debug.Log("Loaded");
            OnLoadGame?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            PlayerInventoryData.SetEmptyInventory();
        }
    }

    private List<InventoryItemDTO> MapInventoryItemsToDTO(List<InventoryItem> inventoryItems)
    {
        List<InventoryItemDTO> inventoryItemSaveDTOs = new();

        foreach (InventoryItem inventoryItem in inventoryItems)
        {
            var inventoryItemDTO = GetInventoryItemDTO(inventoryItem);
            inventoryItemSaveDTOs.Add((InventoryItemDTO)inventoryItemDTO);
        }

        return inventoryItemSaveDTOs;
    }

    private InventoryItemDTO GetInventoryItemDTO(InventoryItem inventoryItem)
    {
        return new InventoryItemDTO()
        {
            id = inventoryItem.id,
            item = inventoryItem.item,
            quantity = inventoryItem.quantity,
            rarity = inventoryItem.rarity,
            addModifiers = inventoryItem.addModifiers
        };

    }

    private List<InventoryItem> MapInventoryItemsFromDTOs(List<InventoryItemDTO> dtos)
    {
        List<InventoryItem> inventoryItems = new();

        foreach (var dto in dtos)
        {
            inventoryItems.Add(new InventoryItem(
                dto.item,
                dto.quantity,
                dto.id,
                dto.rarity,
                dto.addModifiers
            )
           );
        }

        return inventoryItems;
    }

    private List<EquipmentSlotDTO> MapEquipmentsToDTOs(List<PlayerInventoryData.EquipmentSlot> equipments)
    {
        List<EquipmentSlotDTO> equipmentSlotDTOs = new();

        foreach (var equip in equipments)
        {
            if (equip.inventoryItem != null)
            {
                equipmentSlotDTOs.Add(new EquipmentSlotDTO()
                {
                    slot = equip.slot,
                    inventoryItemDTO = GetInventoryItemDTO(equip.inventoryItem)
                });
            }
        }

        return equipmentSlotDTOs;
    }

    private List<PlayerInventoryData.EquipmentSlot> MapEquipmentsFromDTOs(List<EquipmentSlotDTO> equipmentDTOs)
    {
        List<PlayerInventoryData.EquipmentSlot> equipmentSlotDTOs = new();

        foreach (var equip in equipmentDTOs)
        {
            InventoryItemDTO? inventoryItemDTONullable = (InventoryItemDTO?)equip.inventoryItemDTO;

            InventoryItem inventoryItem;

            if (!inventoryItemDTONullable.HasValue)
            {
                inventoryItem = null;
            }
            else
            {
                InventoryItemDTO inventoryItemDTO = (InventoryItemDTO)inventoryItemDTONullable;
                inventoryItem = new InventoryItem(
                    item: inventoryItemDTO.item,
                    id: inventoryItemDTO.id,
                    quantity: inventoryItemDTO.quantity,
                    rarity: inventoryItemDTO.rarity,
                    addModifiers: inventoryItemDTO.addModifiers
                    );
            }

            equipmentSlotDTOs.Add(new PlayerInventoryData.EquipmentSlot()
            {
                slot = equip.slot,
                inventoryItem = inventoryItem,
            });
        }

        return equipmentSlotDTOs;
    }

}
