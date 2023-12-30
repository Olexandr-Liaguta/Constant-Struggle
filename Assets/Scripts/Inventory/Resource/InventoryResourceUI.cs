using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryResourceUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resourceValueText;

    public ResourceType type;
    
    public void SetResourceValue(int value)
    {
        resourceValueText.text = value.ToString();
    }
}
