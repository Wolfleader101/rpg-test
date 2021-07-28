using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Items;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HotbarItem : MonoBehaviour
{
    [HideInInspector] public event Action<int> OnButtonClicked;
    [HideInInspector] public BaseItem currentItem;
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



    // Update is called once per frame
    void Update()
    {
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
