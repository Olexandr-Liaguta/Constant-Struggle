using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryDamageStatUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textComponent;

    public void SetStats(float minValue, float maxValue)
    {
        textComponent.text = minValue.ToString() + " - " + maxValue.ToString();
    }
}
