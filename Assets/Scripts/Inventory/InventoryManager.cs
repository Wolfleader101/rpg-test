using System.Collections;
using System.Collections.Generic;
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

    public void AddItem (BaseItem item)
    {
        if (inventory.AddItem(item))
            Debug.Log ($"Added {item.name} successfully");
        else
            Debug.Log($"Not enough room for {item.name}");
    }
}
