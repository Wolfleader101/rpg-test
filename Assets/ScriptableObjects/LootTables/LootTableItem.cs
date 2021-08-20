using ScriptableObjects.Items;
using UnityEngine;

namespace ScriptableObjects.LootTables
{
    [CreateAssetMenu(fileName = "LootTableItem", menuName = "LootTable/Item"), ExecuteInEditMode, System.Serializable]
    public class LootTableItem : ScriptableObject
    {
        public BaseItem Item;
        public int MinCount = 1;
        public int MaxCount;
        public float DropChance;

    }
}