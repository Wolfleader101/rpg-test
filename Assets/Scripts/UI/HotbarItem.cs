using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarItem : MonoBehaviour
{
    public event Action<int> OnButtonClicked;

    private KeyCode _keyCode;
    private int _keyNumber;

    private void OnValidate()
    {
        _keyNumber = transform.GetSiblingIndex() + 1;
        _keyCode = KeyCode.Alpha0 + _keyNumber;

        gameObject.name = $"Hotbar Item {_keyNumber}";
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(HandleClick);
    }



    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(_keyCode))
        // {
        //     HandleClick();
        // }
    }

    private void HandleClick()
    {
        OnButtonClicked?.Invoke(_keyNumber);
    }
}
