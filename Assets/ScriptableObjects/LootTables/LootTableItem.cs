using ScriptableObjects.Items;
using UnityEngine;

namespace ScriptableObjects.LootTables
{
    [CreateAssetMenu(fileName = "LootTableItem", menuName = "LootTable/Item"), ExecuteInEditMode, System.Serializable]
    public class LootTableItem : ScriptableObject
    {
        public BaseItem Item;
        public int MinDrop = 1;
        public int MaxDrop = 50;
        public float DropChance;

    }
}