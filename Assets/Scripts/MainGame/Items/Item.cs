using UnityEngine;
using UnityEditor;
using System;

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
[Serializable]
public class Item : ScriptableObject
{
    new public string name = "New item";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public int maxQuantity = 1;

    public float weight = 0;

    public virtual void Use()
    {
        // Debug.Log("Use item " + name);
    }

    private void OnEnable()
    {
        EditorApplication.playModeStateChanged += PlayStateChanged;
    }

    void PlayStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            OnLaunch();
        }
    }

    public virtual void OnLaunch() { }
}
