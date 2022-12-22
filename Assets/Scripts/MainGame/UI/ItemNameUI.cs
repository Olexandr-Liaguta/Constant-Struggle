using UnityEngine;
using TMPro;

public class ItemNameUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComponent;

    InventoryItem inventoryItem;

    private void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void SetInventoryItem(InventoryItem _inventoryItem)
    {
        inventoryItem = _inventoryItem;

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
