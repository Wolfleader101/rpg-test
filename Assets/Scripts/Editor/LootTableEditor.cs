using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.LootTables;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BaseLootTable))]
public class LootTableEditor : Editor
{
    private BaseLootTable lootTableComp;
    private static bool showItems = false;

    void OnEnable()
    {
        lootTableComp = (BaseLootTable) target;
        if (lootTableComp.items == null)
        {
            lootTableComp.items = new List<LootTableItem>();
        }
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        lootTableComp.lootTableName = EditorGUILayout.TextField("LootTable Name", lootTableComp.lootTableName);

        showItems = EditorGUILayout.Foldout(showItems, "Items Editor");
        if (showItems)
        {
            for (int i = 0; i < lootTableComp.items.Count; i++)
            {
                lootTableComp.items[i] = (LootTableItem)EditorGUILayout.ObjectField(lootTableComp.items[i], typeof(LootTableItem), false);
                lootTableComp.items[i].name = EditorGUILayout.TextField("Item Name", lootTableComp.items[i].name);
                lootTableComp.items[i].DropChance = EditorGUILayout.Slider("DropChance", lootTableComp.items[i].DropChance, 0, 1);
            }

            if (GUILayout.Button("Add Item"))
            {
                LootTableItem item = (LootTableItem) EditorGUILayout.ObjectField(null, typeof(LootTableItem), false);
                lootTableComp.items.Add(item);
            }
            

        }

        

        
    }
}