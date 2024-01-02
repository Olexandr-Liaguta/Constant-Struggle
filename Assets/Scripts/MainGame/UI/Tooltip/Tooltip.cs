using System.Collections;
using System.Collections.Generic;
using UnityEngine;
      
public class Tooltip : MonoBehaviour
{
    [SerializeField] GameObject attributePrefab, dividerPrefab, contentGO;

    [SerializeField] ItemNameUI itemNameUI;

    List<GameObject> instantiatedTooltipStatGOs = new();

    private class AttributeTexts
    {
        public Attribute attribute;
        public string text;
    }

    List<AttributeTexts> attributeTexts = new()
    {
        new AttributeTexts()  { attribute = Attribute.AttackSpeed, text = "Attack speed"},
        new AttributeTexts() { attribute = Attribute.Damage, text = "Damage" },

        new AttributeTexts()  { attribute = Attribute.Armor, text = "Armor" } ,

        new AttributeTexts()  { attribute = Attribute.Health, text = "Health" },
        new AttributeTexts()  { attribute = Attribute.Mana, text = "Mana" },

        new AttributeTexts() { attribute = Attribute.Accuracy, text = "Accuracy" },
        new AttributeTexts() { attribute = Attribute.Spirit, text = "Spirit" },
        new AttributeTexts()  {  attribute = Attribute.Strength, text = "Strength" },
        new AttributeTexts() { attribute = Attribute.Agility, text = "Agility" },

        new AttributeTexts() { attribute = Attribute.HealthRegeneration, text = "Health regeneration" },
        new AttributeTexts() { attribute = Attribute.ManaRegeneration, text = "Mana regeneration"},
    };


    void Start()
    {
        foreach (Transform child in contentGO.transform)
        {
            Destroy(child.gameObject);
        }
        gameObject.SetActive(false);
    }


    public void Show(InventoryItem inventoryItem)
    {
        itemNameUI.SetInventoryItem(inventoryItem);

        bool hasInventoryItemMidifiers = inventoryItem.addModifiers != null && inventoryItem.addModifiers.Count > 0;

        if (inventoryItem.item is Equipment)
        {
            var equipment = inventoryItem.item as Equipment;

            foreach (ItemManager.AddModifier addModifier in equipment.addModifiers)
            {
                InstantiateTooltipStatPrefab(addModifier, false);
            }

            if (hasInventoryItemMidifiers)
            {
                InstantiateDivider();
            }
        }

        if (hasInventoryItemMidifiers)
        {
            foreach (var modifierAndValue in inventoryItem.addModifiers)
            {
                InstantiateTooltipStatPrefab(modifierAndValue, true);
            }
        }

        gameObject.SetActive(true);
    }

    void InstantiateTooltipStatPrefab(ItemManager.AddModifier addModifier, bool isAdditional)
    {
        GameObject instantiatedAttributePrefab = InstantiatePrefab(attributePrefab);

        var tooltipStatUI = instantiatedAttributePrefab.GetComponent<TooltipStatUI>();

        string attributeTitle = attributeTexts.Find(value => value.attribute == addModifier.attribute).text;
        tooltipStatUI.SetStatText(attributeTitle, addModifier.value, isAdditional);
    }

    private void InstantiateDivider()
    {
        InstantiatePrefab(dividerPrefab);
    }

    private GameObject InstantiatePrefab(GameObject prefab)
    {
        var instantiatedPrefab = Instantiate(prefab);
        instantiatedPrefab.transform.SetParent(contentGO.transform);

        // Its get bigger scale 
        instantiatedPrefab.transform.localScale = Vector3.one;

        instantiatedTooltipStatGOs.Add(instantiatedPrefab);

        return instantiatedPrefab;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        itemNameUI.Clear();

        instantiatedTooltipStatGOs.ForEach(Go => { Destroy(Go); });
        instantiatedTooltipStatGOs.Clear();
    }
}
