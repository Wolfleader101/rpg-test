using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Inventory;
using ScriptableObjects.Items;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    [SerializeField] private Inventory inventory;

    private GameObject _currentSelectedButton;

    private void Awake()
    {
        foreach (var button in GetComponentsInChildren<HotbarItem>())
        {
            button.OnButtonClicked += OnButtonClicked;
        }
        
        inventory.OnItemAdded += AddItem;
        inventory.OnItemRemoved += ItemRemoved;
        
        _currentSelectedButton = transform.Find($"Hotbar Item 1").gameObject;
        _currentSelectedButton.gameObject.SetActive(true);
    }

    private void OnButtonClicked(HotbarItem hotbarItem)
    {
        if (_currentSelectedButton != null)
            _currentSelectedButton.transform.Find("Selected").gameObject.SetActive(false);

        _currentSelectedButton = hotbarItem.gameObject;

        _currentSelectedButton.transform.Find("Selected").gameObject.SetActive(true);
    }
    
    private void AddItem(BaseItem item, int itemCount)
    {
        // list of hotbar slots
        var hotbarItems = gameObject.GetComponentsInChildren<HotbarItem>().ToList();

        // this is for splitting items
        var queuedItems = new List<Dictionary<BaseItem, int>>();

        // if itemCount is greater than maxStackSize then it will need to split them
        while (itemCount > item.MaxStackSize)
        {
            queuedItems.Add(new Dictionary<BaseItem, int>()
            {
                {
                    item, item.MaxStackSize
                }
            });

            Debug.Log($"{itemCount} Added Queued Item {item.MaxStackSize}");
            itemCount -= item.MaxStackSize;
        }

        // Add in the remainder
        queuedItems.Add(new Dictionary<BaseItem, int>()
        {
            {
                item, itemCount
            }
        });

        // loop over each inventory slot
        foreach (var button in hotbarItems)
        {
            // if slot is empty
            if (button.currentItem == null)
            {
                foreach (var queuedItem in queuedItems.Where(queuedItem => queuedItem.ContainsKey(item)))
                {
                    button.AddItem(item, queuedItem[item]);
                    queuedItems.Remove(queuedItem);
                    break;
                }

                continue;
            }

            // if slots item is not current item AND if its already at max cap
            if (button.currentItem != item && button.itemCount >= button.currentItem.MaxStackSize) continue;

            foreach (var queuedItem in queuedItems)
            {
                var incrementRem = button.IncrementCount(queuedItem[item]);
                if (incrementRem == 0)
                {
                    queuedItems.Remove(queuedItem);
                    break;
                }

                queuedItem[item] = incrementRem;
                break;
            }
        }
        
        // if item could not be added
        // NOTE THIS SHOULD NEVER HAPPEN
        // Inventory maxCapacity should trigger Inventory method to return false
        
        // remove count from Inventory (will invoke RemoveItemEvent, which will drop item)
        //inventory.RemoveItem(item, itemCount);
    }

    private void ItemRemoved(BaseItem item, int itemCount)
    {
        
    }
    
    private void HandleDrop()
    {
        var currentItem = _currentSelectedButton.GetComponent<HotbarItem>().currentItem;
    }

    public void OnItemDropPressed(InputAction.CallbackContext context)
    {
        if(context.performed) HandleDrop();
    }
}