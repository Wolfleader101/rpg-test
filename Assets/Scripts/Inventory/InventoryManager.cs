using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<ItemHandler> _items;
    public List<ItemHandler> Items => _items;
    
    // Start is called before the first frame update
    void Start()
    {
        _items = new List<ItemHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
