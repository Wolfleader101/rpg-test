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
        //Debug.Log(item);
        // list of hotbar slots
        var hotbarItems = gameObject.GetComponentsInChildren<HotbarItem>().ToList();
        
        // loop over each inventory slot
        foreach (var button in hotbarItems)
        {
            // if slot is empty
            if (button.currentItem == null)
            {
                button.AddItem(item, itemCount);

                return;
            }
            
            // if slots item is not current item OR if its already at max cap
            if (button.currentItem != item || button.itemCount >= button.currentItem.MaxStackSize) continue;
            
            var incrementRem = button.IncrementCount(itemCount);
            if (incrementRem == 0)
            {
                break;
            }
            
            itemCount = incrementRem;
            
        }
        
        // if item could not be added
        // NOTE THIS SHOULD NEVER HAPPEN
        // Inventory maxCapacity should trigger Inventory method to return false
        
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