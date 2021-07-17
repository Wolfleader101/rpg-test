using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private float baseHealth = 100f;
    public float BaseHealth => baseHealth;
    
    [SerializeField] private float baseStamina = 100f; 
    public float BaseStamina => baseStamina;
    
    [SerializeField] private float baseMagic = 50f;
    public float BaseMagic => baseMagic;

    [SerializeField] private float currentHealth = 0f;
    public float CurrentHealth => currentHealth;
    
    [SerializeField] private float currentStamina = 0f;
    public float CurrentStamina => currentStamina;
    
    [SerializeField] private float currentMagic = 0f;
    public float CurrentMagic => currentMagic;


    private float _healthBuff = 0f;
    private float _staminaBuff = 0f;
    private float _magicBuff = 0f;
    public float HealthBuff
    {
        get => _healthBuff;
        set
        {
            _healthBuff = value;
            currentHealth += value;
        }
    }


    public float StaminaBuff
    {
        get => _staminaBuff;
        set
        {
            _staminaBuff = value;
            currentStamina += value;
        }
    }

    public float MagicBuff
    {
        get => _magicBuff;
        set
        {
            _magicBuff = value;
            currentMagic += value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = baseHealth + _healthBuff;
        currentStamina = baseStamina + _staminaBuff;
        currentMagic = baseMagic + _magicBuff;
    }

    // Update is called once per frame
    void Update()
    {
    }
}