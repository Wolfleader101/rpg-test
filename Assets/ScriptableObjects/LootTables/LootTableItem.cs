using ScriptableObjects.Items;
using UnityEngine;

namespace ScriptableObjects.LootTables
{
    [ExecuteInEditMode, System.Serializable]
    public class LootTableItem 
    {
        public BaseItem Item;
        public int MinDrop = 1;
        public int MaxDrop = 50;
        public float DropChance;


        public LootTableItem()
        {
            Item = null;
        }
    }
}