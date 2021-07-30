using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Items;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D), typeof(Collider2D))]
public class WorldItem : MonoBehaviour
{
    [SerializeField] private BaseItem item;
    [SerializeField] private int itemCount = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = item.Sprite;
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        var textObj = gameObject.transform.Find("ItemCount").gameObject;
        if (textObj != null)
        {
            var text = textObj.GetComponent<TextMeshPro>();
            textObj.SetActive(true);
            text.text = itemCount.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        var colliderGameObject = collider.gameObject;
        var inventoryManager = colliderGameObject.GetComponent<InventoryManager>();
        if (inventoryManager == null) inventoryManager = colliderGameObject.GetComponentInChildren<InventoryManager>();
        
        inventoryManager.AddItem(item, itemCount);
        
        Destroy(this.gameObject);
    }
}
