using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Tool,
    Clothing,
    Armour,
    Potion,
    Food,
    Material,
    Collectible,
    Misc
}
[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class BaseItem : ScriptableObject
{
    [SerializeField] private ItemType type;
    public ItemType Type => type;
    
    [SerializeField] private string itemName;
    public string ItemName => itemName;
    
    [SerializeField, Multiline] private string description;
    public string Description => description;

    [SerializeField] private int maxStackSize = 32;
    public int MaxStackSize => maxStackSize;
    
    // set to public so InventoryManager can manage stackSize
    public bool canStack = true;

    [SerializeField] private Sprite sprite;
    public Sprite Sprite => sprite;
    
}
