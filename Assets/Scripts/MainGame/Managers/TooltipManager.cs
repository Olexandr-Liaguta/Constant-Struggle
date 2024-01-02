using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance { get; private set; }

    [SerializeField] private Tooltip tooltip;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Hide();
    }

    public void Show(InventoryItem inventoryItem)
    {
        tooltip.Show(inventoryItem);
    }

    public void Hide()
    {
        tooltip.Hide();
    }
}
