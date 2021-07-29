using System;
using System.Collections;
using System.Collections.Generic;
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
        Dictionary<BaseItem, int> queuedItems = new Dictionary<BaseItem, int>();

        foreach (var button in GetComponentsInChildren<HotbarItem>())
        {
            if (itemCount > item.MaxStackSize)
            {
                queuedItems.Add(item, (item.MaxStackSize - itemCount));
            }

            if (button.currentItem == null)
            {
                if (queuedItems.Count > 0)
                {
                    queuedItems.Remove(item);
                    continue;
                }

                button.AddItem(item, itemCount);
                return;
            }

            if (button.currentItem == item)
            {
                bool canIncrement = button.IncrementCount(itemCount);
                if (canIncrement) return;

                // THIS SYSTEM BEST WORKS WHEN TRYING TO MASS ADD ITEMS
                // if it can't increment then add it to next slot
                // to do this i will implement a queue system
                // it will add an item to the queue
                // on next for each check if the slot is empty AND THERE IS QUEUED ITEM/S
                // if so add it to that slot and remove it from queued items
                // otherwise continue
                // if by end of the foreach loop there is no slot for it,
                // then drop item
            }
        }
    }
}