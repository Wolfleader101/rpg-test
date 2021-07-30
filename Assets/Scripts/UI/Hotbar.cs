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
            itemCount = itemCount - item.MaxStackSize;
        }

        Debug.Log($"Added {queuedItems.Count}x Items to Queue");
        Debug.Log($"Added Items {itemCount}");


        foreach (var button in hotbarItems)
        {
            if (button.currentItem == null)
            {
                if (queuedItems.Count > 0)
                {
                    foreach (var queuedItem in queuedItems)
                    {
                        if (!queuedItem.ContainsKey(item)) continue;
                        button.AddItem(item, queuedItem[item]);
                        queuedItems.Remove(queuedItem);
                        break;
                    }
                    continue;
                }

                button.AddItem(item, itemCount);
                return;
            }


            if (button.currentItem == item)
            {
                if (queuedItems.Count > 0)
                {
                    foreach (var queuedItem in queuedItems)
                    {
                        int incrementRemQ = button.IncrementCount(queuedItem[item]);
                        if (incrementRemQ == 0)
                        {
                            queuedItems.Remove(queuedItem);
                            break;
                        }

                        queuedItem[item] -= incrementRemQ;
                        break;
                    }
                    continue;
                }

                int incrementRem = button.IncrementCount(itemCount);
                if (incrementRem == 0)
                {
                    return;
                }

                queuedItems.Add(new Dictionary<BaseItem, int>()
                {
                    {
                        item, incrementRem
                    }
                });
            }
        }

        // if item could not be added
        // drop the item
        //DropItem(Item, itemCount);
        // remove count from Inventory
        inventory.RemoveItem(item, itemCount);
    }
}