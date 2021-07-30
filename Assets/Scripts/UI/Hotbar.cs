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
        Dictionary<BaseItem, int> queuedItems = new Dictionary<BaseItem, int>();
        var hotbarItems = gameObject.GetComponentsInChildren<HotbarItem>().ToList();

        if (itemCount > item.MaxStackSize)
        {
            queuedItems.Add(item, item.MaxStackSize);
            itemCount = itemCount - item.MaxStackSize;
        }
        

        for (int i = 0; i < hotbarItems.Count; i++)
        {
            var button = hotbarItems[i];
            
            if (button.currentItem == null)
            {
                if (queuedItems.Count > 0)
                {
                    button.AddItem(item, queuedItems[item]);
                    queuedItems.Remove(item);
                    continue;
                }

                button.AddItem(item, itemCount);
                return;
            }
            

            if (button.currentItem == item)
            {
                if (queuedItems.Count > 0)
                {
                    int incrementRemQ = button.IncrementCount(queuedItems[item]);
                    if (incrementRemQ == 0)
                    {
                        queuedItems.Remove(item);
                        continue;
                    }

                    queuedItems[item] -= incrementRemQ;
                    continue;
                }
                int incrementRem = button.IncrementCount(itemCount);
                if (incrementRem == 0)
                {
                    return;
                }
                queuedItems.Add(item, incrementRem);
            }

        }
        
        // if item could not be added
        // drop the item
        //DropItem(Item, itemCount);
        // remove count from Inventory
        inventory.RemoveItem(item, itemCount);
    }
}