using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
        
    public virtual void Use()
    {
        Debug.Log("Use item " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this); 
    }
}
