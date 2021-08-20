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
                lootTableComp.items[i].MinDrop = EditorGUILayout.IntSlider("Min Drop", lootTableComp.items[i].MinDrop, 1, lootTableComp.items[i].MaxDrop - 1);
                lootTableComp.items[i].MaxDrop = EditorGUILayout.IntSlider("Max Drop", lootTableComp.items[i].MaxDrop, lootTableComp.items[i].MinDrop, 100);
                lootTableComp.items[i].DropChance = EditorGUILayout.Slider("DropChance", lootTableComp.items[i].DropChance, 0, 1);
                // max value should be based on the element size and all their values
            }

            if (GUILayout.Button("Add Item"))
            {
                //LootTableItem item = (LootTableItem) EditorGUILayout.ObjectField(null, typeof(LootTableItem), false);
                lootTableComp.items.Add(ScriptableObject.CreateInstance<LootTableItem>());
            }
            

        }

        

        
    }
}