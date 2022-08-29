using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryDamageStatUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI min, max;

    public void SetStats(int minValue, int maxValue)
    {
        min.text = minValue.ToString();
        max.text = maxValue.ToString();
    }
}
