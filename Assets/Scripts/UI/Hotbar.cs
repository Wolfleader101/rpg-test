using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Inventory;
using ScriptableObjects.Items;
using UnityEngine;
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
    }

    private void OnButtonClicked(int buttonNumber)
    {
        if (_currentSelectedButton != null)
            _currentSelectedButton.transform.Find("Selected").gameObject.SetActive(false);

        _currentSelectedButton = transform.Find($"Hotbar Item {buttonNumber}").gameObject;

        _currentSelectedButton.transform.Find("Selected").gameObject.SetActive(true);
    }

    private void AddItem(BaseItem item, int itemCount)
    {
        // if it can't increment then add it to next slot
        // to do this i will implement a queue system
        // it will add an item to the queue
        // on next for each check if the slot is empty AND THERE IS QUEUED ITEM/S
        // if so add it to that slot and remove it from queued items
        // otherwise continue
        // if by end of the foreach loop there is no slot for it,
        // then drop item
        List<Dictionary<BaseItem, int>> queuedItems = new List<Dictionary<BaseItem, int>>();
        var hotbarItems = gameObject.GetComponentsInChildren<HotbarItem>().ToList();
        while (itemCount > item.MaxStackSize)
        {
            queuedItems.Add(new Dictionary<BaseItem, int>()
            {
                {
                    item, item.MaxStackSize
                }
            });
            Debug.Log($"{itemCount} Added Queued Item {item.MaxStackSize}");
            itemCount = itemCount - item.MaxStackSize;
        }

        queuedItems.Add(new Dictionary<BaseItem, int>()
        {
            {
                item, itemCount
            }
        });


        foreach (var button in hotbarItems)
        {
            if (button.currentItem == null)
            {
                // Debug.Log($"Queued item count : {queuedItems.Count}");
                foreach (var queuedItem in queuedItems.Where(queuedItem => queuedItem.ContainsKey(item)))
                {
                    // Debug.Log($"Queued item name {queuedItem[item]}");
                    button.AddItem(item, queuedItem[item]);
                    queuedItems.Remove(queuedItem);
                    break;
                }

                continue;
            }

            if (button.currentItem == item && button.itemCount < button.currentItem.MaxStackSize)
            {
                if (queuedItems.Count > 0)
                {
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
                
            }
        }

        // if item could not be added
        // drop the item
        //DropItem(Item, itemCount);
        
        // remove count from Inventory
        inventory.RemoveItem(item, itemCount);
    }
}