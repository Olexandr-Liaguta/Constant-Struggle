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



    Dictionary<Modifier, string> modifierStrings = new()
    {
        {Modifier.Armor, "Armor" },
        {Modifier.AttackSpeed, "Attack speed" },
        {Modifier.Health, "Health" },
        {Modifier.Mana, "Mana" },
        {Modifier.Damage, "Damage" },
        {Modifier.Accuracy, "Accuracy" },
        {Modifier.Spirit, "Spirit" },
        {Modifier.Strength, "Strength" },
        {Modifier.Agility, "Agility" },
        {Modifier.HealthRegeneration, "Health regeneration" },
        {Modifier.ManaRegeneration, "Mana regeneration" },
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

            foreach(var modifierAndValue in equipment.modifiersMap)
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

    void InstantiateTooltipStatPrefab(Transform parentTransform, KeyValuePair<Modifier, ModifierValue> modifierAndValue, bool isAdditional)
    {
        var instantiatedTooltipPrefab = Instantiate(tooltipStatPrefab);

        instantiatedTooltipPrefab.transform.SetParent(parentTransform);

        var tooltipStatUI = instantiatedTooltipPrefab.GetComponent<TooltipStatUI>();

        tooltipStatUI.SetStatText(modifierStrings[modifierAndValue.Key], modifierAndValue.Value, isAdditional);

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
