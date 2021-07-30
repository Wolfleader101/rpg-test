using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Inventory;
using ScriptableObjects.Items;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    public Inventory Inventory => inventory;
    
    // Start is called before the first frame update
    void Start()
    {
        inventory.Init();
    }

    public void AddItem (BaseItem item, int itemCount)
    {
        if (inventory.AddItem(item, itemCount))
            Debug.Log ($"Added {item.name} of type {item.GetType()} successfully");
        else
            Debug.Log($"Not enough room for {item.name}");
    }
}
