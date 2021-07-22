using UnityEngine;

namespace ScriptableObjects.Items
{
    public abstract class BaseItem : ScriptableObject
    {

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
}
