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
        var addItem = inventory.AddItem(item, itemCount);
        if (addItem == 0)
            Debug.Log ($"Added {itemCount}x {item.name}");
        else
            Debug.Log($"Not enough room for {item.name}, left over: {addItem}");
        
        
    }
}
