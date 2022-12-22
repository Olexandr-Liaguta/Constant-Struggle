using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ResourceType {
    Wood,
    Steel,
    Food,
    Crystal
}

[CreateAssetMenu(fileName = "New resource item", menuName = "Inventory/Resource/New")]
public class ResourceItem : Item
{
    public ResourceType type;
}
