using UnityEngine;
using TMPro;

public class InventoryWeightUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentWeight, maxWeight;
    [SerializeField] PlayerStats playerStats;

    [SerializeField] private InventorySO inventory;

    public void UpdateWeightValues()
    {
        if (inventory == null) return;

        currentWeight.text = inventory.GetCurrentWeight().ToString();
        maxWeight.text = playerStats.maxHandleWeight.GetValue().ToString();
    }
}
