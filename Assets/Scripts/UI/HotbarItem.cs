using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Items;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HotbarItem : MonoBehaviour
{
    [HideInInspector] public event Action<int> OnButtonClicked;
    [HideInInspector] public BaseItem currentItem { get; set; }
    [HideInInspector] public int itemCount;
    
    private int _keyNumber;

    private void OnValidate()
    {
        _keyNumber = transform.GetSiblingIndex() + 1;

        gameObject.name = $"Hotbar Item {_keyNumber}";
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    public void AddItem(BaseItem item, int count)
    {
        var itemObj = gameObject.transform.Find("Item").gameObject;
        var textObj = gameObject.transform.Find("ItemCount").gameObject;
        
        var img = itemObj.GetComponent<Image>();
        var text = textObj.GetComponent<TextMeshProUGUI>();
        img.sprite = item.Sprite;
        
        itemObj.SetActive(true);
        textObj.SetActive(true);

        currentItem = item;
        itemCount = count;
        text.text = itemCount.ToString();

    }

    public int IncrementCount(int count)
    {
        if (itemCount == currentItem.MaxStackSize) return count;
        int maxIncrement = currentItem.MaxStackSize - itemCount; 
        int clamped = Mathf.Clamp(count, 1, maxIncrement);
        
        itemCount += clamped;
        
        var textObj = gameObject.transform.Find("ItemCount").gameObject;
        var text = textObj.GetComponent<TextMeshProUGUI>();
        text.text = itemCount.ToString();
        
        return count - clamped;
    }
    private void HandleClick()
    {
        OnButtonClicked?.Invoke(_keyNumber);
    }

    public void OnHotbarPress(InputAction.CallbackContext context)
    {
        int key = Int32.Parse(context.control.name);
        if (key == _keyNumber)
        {
            HandleClick();
        }
    }
}
