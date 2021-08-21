using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Items;
using ScriptableObjects.LootTables;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BaseLootTable))]
public class LootTableEditor : Editor
{
    private BaseLootTable lootTableComp;
    private static bool showItems = false;
    private static GUILayoutOption buttonWidth = GUILayout.Width(100);

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
                EditorGUILayout.BeginHorizontal();
                lootTableComp.items[i].Item = (BaseItem)EditorGUILayout.ObjectField(lootTableComp.items[i].Item, typeof(BaseItem), false);
                if (GUILayout.Button("Remove Item", buttonWidth))
                {
                    lootTableComp.items.Remove(lootTableComp.items[i]);
                }
                EditorGUILayout.EndHorizontal();
                lootTableComp.items[i].MinDrop = EditorGUILayout.IntSlider("Min Drop", lootTableComp.items[i].MinDrop, 1, lootTableComp.items[i].MaxDrop - 1);
                lootTableComp.items[i].MaxDrop = EditorGUILayout.IntSlider("Max Drop", lootTableComp.items[i].MaxDrop, lootTableComp.items[i].MinDrop, 100);
                lootTableComp.items[i].DropChance = EditorGUILayout.Slider("DropChance", lootTableComp.items[i].DropChance, 0, 1);
                // max value should be based on the element size and all their values
                
                GUILayout.Space(20);
            }
            
            GUILayout.Space(10);

            if (GUILayout.Button("Add Item", buttonWidth))
            {
                //LootTableItem item = (LootTableItem) EditorGUILayout.ObjectField(null, typeof(LootTableItem), false);
                lootTableComp.items.Add(new LootTableItem());
            }
        }

        EditorUtility.SetDirty(lootTableComp);
        

        
    }
}