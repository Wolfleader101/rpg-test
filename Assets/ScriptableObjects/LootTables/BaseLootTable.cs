using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjects.LootTables
{
    [CreateAssetMenu(fileName = "LootTable", menuName = "LootTable/Table"), ExecuteInEditMode, System.Serializable]
    public class BaseLootTable : ScriptableObject
    {
        public string lootTableName;
        public List<LootTableItem> items;
        
    }
}