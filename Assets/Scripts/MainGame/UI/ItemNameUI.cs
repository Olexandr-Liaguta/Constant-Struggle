using UnityEngine;
using TMPro;

public class ItemNameUI : MonoBehaviour
{
    TextMeshProUGUI textComponent;

    InventoryItem inventoryItem;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void SetInventoryItem(InventoryItem inventoryItem)
    {
        this.inventoryItem = inventoryItem;

        HandleText();
    }

    void HandleText()
    {
        string quantityText = inventoryItem.quantity > 1 ? " x" + inventoryItem.quantity : "";

        textComponent.text = inventoryItem.item.name + quantityText;
        textComponent.color = ItemHelpers.rarityColors[inventoryItem.rarity];
    }

    public void Clear()
    {
        textComponent.text = string.Empty;
    }


}
