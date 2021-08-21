using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Inventory;
using ScriptableObjects.Items;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Inventory)), CanEditMultipleObjects]
public class InventoryEditor : Editor
{
    private Inventory inventoryComp;
    private static bool showItems = false;
    //private static GUILayoutOption buttonWidth = GUILayout.Width(100);

    void OnEnable()
    {
        inventoryComp = (Inventory) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //lootTableComp.lootTableName = EditorGUILayout.TextField("LootTable Name", lootTableComp.lootTableName);

        showItems = EditorGUILayout.Foldout(showItems, "Items Viewer");
        if (showItems)
        {
            if(inventoryComp.Items.Count == 0) return;
            
            foreach (var element in inventoryComp.Items.SelectMany(item => item))
            {
                EditorGUILayout.ObjectField(element.Key, typeof(BaseItem), false);
                EditorGUILayout.TextField(element.Key.ItemName);
                EditorGUILayout.IntField("Max Stack Size", element.Key.MaxStackSize);
                EditorGUILayout.IntField("Current Count", element.Value);
                

                GUILayout.Space(20);
            }
            
            GUILayout.Space(10);
            
        }
        //EditorUtility.SetDirty(inventoryComp);
    }
}