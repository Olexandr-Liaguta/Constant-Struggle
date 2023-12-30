using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum PrimalStat
{
    Agility,
    Accuracy,
    Spirit,
    Strength,
}

public class InventoryPrimalStatUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI baseText, totalText;

    public PrimalStat statType;

    public void SetStats(int baseValue, int totalValue)
    {
        baseText.text = "(" + baseValue.ToString() + ")";
        totalText.text = totalValue.ToString();
    }
}
