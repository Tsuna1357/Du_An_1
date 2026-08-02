using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    [Header("Basic Info")]
    public int itemID;
    public string itemName;
    [TextArea]
    public string description;

    [Header("Inventory")]
    public Sprite icon;

    [Header("Setting")]
    public bool consumable;
}