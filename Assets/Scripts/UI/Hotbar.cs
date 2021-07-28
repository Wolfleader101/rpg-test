using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Inventory;
using ScriptableObjects.Items;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    [SerializeField]private Inventory inventory;
    
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
       if(_currentSelectedButton != null) _currentSelectedButton.transform.Find("Selected").gameObject.SetActive(false);
        
        _currentSelectedButton = transform.Find($"Hotbar Item {buttonNumber}").gameObject;
        
        _currentSelectedButton.transform.Find("Selected").gameObject.SetActive(true);
    }

    private void AddItem(BaseItem item, int itemCount)
    {
        foreach (var button in GetComponentsInChildren<HotbarItem>())
        {
            
            if(button.currentItem != item) continue;


        }
    }

}
