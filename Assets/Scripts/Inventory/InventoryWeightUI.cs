using UnityEngine;
using TMPro;

public class InventoryWeightUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentWeight, maxWeight;
    [SerializeField] PlayerStats playerStats;

    public void UpdateWeightValues()
    {
        currentWeight.text = PlayerInventoryData.GetCurrentWeight().ToString();
        maxWeight.text = playerStats.maxHandleWeight.GetValue().ToString();
    }
}
