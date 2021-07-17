using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    private GameObject _currentSelectedButton;
    private void Awake()
    {
        foreach (var button in GetComponentsInChildren<HotbarItem>())
        {
            button.OnButtonClicked += OnButtonClicked;
        }
    }

    private void OnButtonClicked(int buttonNumber)
    {
        _currentSelectedButton.transform.Find("Selected").gameObject.SetActive(false);
        
        _currentSelectedButton = transform.Find($"Hotbar Item {buttonNumber}").gameObject;
        
        _currentSelectedButton.transform.Find("Selected").gameObject.SetActive(true);
    }
    
}
