using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Tool,
    Potion,
    Food
}
[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item")]
public class BaseItem : ScriptableObject
{
    [SerializeField] private ItemType type;
    public ItemType Type => type;
    
    [SerializeField] private string itemName;
    public string ItemName => itemName;
    
    [SerializeField, Multiline] private string description;
    public string Description => description;

    [SerializeField] private Sprite sprite;
    public Sprite Sprite => sprite;
}
