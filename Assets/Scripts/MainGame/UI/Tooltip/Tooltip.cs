using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    #region Singletone
    public static Tooltip instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    #endregion

    [SerializeField] GameObject 
        tooltipStatPrefab, 
        defaultStatsTitle,
        defaultStatsParent,
        additionalStatsTitle,
        additionalStatParent, 
        mainTooltip;

    [SerializeField] ItemNameUI itemNameUI;

    List<GameObject> instantiatedTooltipStatGOs = new();



    Dictionary<Attribute, string> modifierStrings = new()
    {
        {Attribute.Armor, "Armor" },
        {Attribute.AttackSpeed, "Attack speed" },
        {Attribute.Health, "Health" },
        {Attribute.Mana, "Mana" },
        {Attribute.Damage, "Damage" },
        {Attribute.Accuracy, "Accuracy" },
        {Attribute.Spirit, "Spirit" },
        {Attribute.Strength, "Strength" },
        {Attribute.Agility, "Agility" },
        {Attribute.HealthRegeneration, "Health regeneration" },
        {Attribute.ManaRegeneration, "Mana regeneration" },
    };


    void Start()
    {
        mainTooltip.SetActive(false);

        HideStatsGOs();
    }

    void HideStatsGOs()
    {
        defaultStatsParent.SetActive(false);
        additionalStatParent.SetActive(false);
        defaultStatsTitle.SetActive(false);
        additionalStatsTitle.SetActive(false);
    }

    public void ShowTooltip(InventoryItem inventoryItem)
    {
        itemNameUI.SetInventoryItem(inventoryItem);

        if(inventoryItem.item is Equipment)
        {
            var equipment = inventoryItem.item as Equipment;

            foreach(ItemManager.AddModifier modifierAndValue in equipment.addModifiers)
            {
                InstantiateTooltipStatPrefab(defaultStatsParent.transform, modifierAndValue, false);
            }

            defaultStatsParent.SetActive(true);
            defaultStatsTitle.SetActive(true);
        }

        if (inventoryItem.addModifiers != null && inventoryItem.addModifiers.Count > 0)
        {
            foreach (var modifierAndValue in inventoryItem.addModifiers)
            {
                InstantiateTooltipStatPrefab(additionalStatParent.transform, modifierAndValue, true);
            }

            additionalStatParent.SetActive(true);
            additionalStatsTitle.SetActive(true);
        }

        mainTooltip.SetActive(true);
    }

    void InstantiateTooltipStatPrefab(Transform parentTransform, ItemManager.AddModifier addModifier, bool isAdditional)
    {
        var instantiatedTooltipPrefab = Instantiate(tooltipStatPrefab);

        instantiatedTooltipPrefab.transform.SetParent(parentTransform);

        var tooltipStatUI = instantiatedTooltipPrefab.GetComponent<TooltipStatUI>();

        tooltipStatUI.SetStatText(modifierStrings[addModifier.attribute], addModifier.value, isAdditional);

        instantiatedTooltipStatGOs.Add(instantiatedTooltipPrefab);
    }

    public void HideTooltip()
    {
        mainTooltip.SetActive(false);
        itemNameUI.Clear();

        HideStatsGOs();

        instantiatedTooltipStatGOs.ForEach(Go => { Destroy(Go); });
        instantiatedTooltipStatGOs.Clear();

    }
}
