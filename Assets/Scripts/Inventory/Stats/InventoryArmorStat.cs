using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryArmorStat : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI armor;

    public void SetStat(int value)
    {
        armor.text = value.ToString();
    }
}
