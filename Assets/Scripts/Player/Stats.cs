using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Health,
    Stamina,
    Mana
}
public class Stats : MonoBehaviour
{
    [SerializeField] private StatsBar healthBar;
    [SerializeField] private StatsBar staminaBar;
    [SerializeField] private StatsBar manaBar;
    
    [SerializeField] private float baseHealth = 100f;
    public float BaseHealth => baseHealth;

    [SerializeField] private float baseStamina = 100f;
    public float BaseStamina => baseStamina;

    [SerializeField] private float baseMana = 50f;
    public float BaseMana => baseMana;

    [SerializeField] private float currentHealth = 0f;
    public float CurrentHealth => currentHealth;

    [SerializeField] private float currentStamina = 0f;
    public float CurrentStamina => currentStamina;

    [SerializeField] private float currentMana = 0f;
    public float CurrentMana => currentMana;

    private float _maxHealth = 0f;
    private float _maxStamina = 0f;
    private float _maxMana = 0f;

    private float _healthBuff = 0f;
    private float _staminaBuff = 0f;
    private float _manaBuff = 0f;

    private float HealthBuff
    {
        get => _healthBuff;
        set
        {
            _healthBuff = value;
            currentHealth += value;
        }
    }

    private float StaminaBuff
    {
        get => _staminaBuff;
        set
        {
            _staminaBuff = value;
            currentStamina += value;
        }
    }

    private float ManaBuff
    {
        get => _manaBuff;
        set
        {
            _manaBuff = value;
            currentMana += value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateMaxHealth(ref _maxHealth, baseHealth, _healthBuff);
        Debug.Log(_maxHealth);
        
        currentHealth = baseHealth + _healthBuff;
        currentStamina = baseStamina + _staminaBuff;
        currentMana = baseMana + _manaBuff;

        healthBar.SetMaxValue(currentHealth);
        staminaBar.SetMaxValue(currentStamina);
        manaBar.SetMaxValue(currentMana);
    }

    private void UpdateMaxHealth(ref float maxValue, float baseValue, float buffValue)
    {
        maxValue = baseValue + buffValue;
    }

public void Damage(float amount)
    {
        currentHealth -= amount;
        
        healthBar.SetValue(currentHealth);

        if (currentHealth <= 0) Kill();
    }

    // potentially make public
    private void Kill()
    {
    }

    public void DrainStat(StatType stat, float amount)
    {
        switch (stat)
        {
            case StatType.Health:
                currentHealth -= amount;
                break;
            case StatType.Stamina:
                currentStamina -= amount;
                break;
            case StatType.Mana:
                currentMana -= amount;
                break;
        }
    }

    public IEnumerator DrainStatOverTime(StatType stat, float totalDrain, float totalTime)
    {
        float timeElapsed;
        float endStat;
        switch (stat)
        {
            case StatType.Health:
                timeElapsed = 0f;
                endStat = (currentHealth - totalDrain);

                while (timeElapsed < totalTime)
                {
                    //Debug.LogError(timeElapsed);
                    healthBar.SetValue(currentHealth);
                    currentHealth = Mathf.Lerp(currentHealth, endStat,
                        timeElapsed / totalTime);

                    if (Time.timeScale == 0)
                    {
                        timeElapsed += Time.unscaledDeltaTime;
                    }
                    else
                    { 
                        timeElapsed += Time.deltaTime;
                    }
                   

                    yield return null;
                }

        
                //currentHealth = endStat;
                healthBar.SetValue(currentHealth);
                
                break;
            case StatType.Stamina:
                timeElapsed = 0f;
                endStat = (currentStamina - totalDrain);

                while (timeElapsed < totalTime)
                {
                    //Debug.LogError(timeElapsed);
                    staminaBar.SetValue(currentStamina);
                    currentStamina = Mathf.Lerp(currentStamina, endStat,
                        timeElapsed / totalTime);

                    if (Time.timeScale == 0)
                    {
                        timeElapsed += Time.unscaledDeltaTime;
                    }
                    else
                    { 
                        timeElapsed += Time.deltaTime;
                    }
                   

                    yield return null;
                }

        
                //currentHealth = endStat;
                staminaBar.SetValue(currentStamina);
                break;
            case StatType.Mana:
                break;
        }
    }

    public void RemoveBuff(StatType stat)
    {
        switch (stat)
        {
            case StatType.Health:
                currentHealth -= HealthBuff;
                HealthBuff = 0f;
                break;
            case StatType.Stamina:
                currentStamina -= StaminaBuff;
                StaminaBuff = 0f;
                break;
            case StatType.Mana:
                currentMana -= ManaBuff;
                ManaBuff = 0f;
                break;
        }
    }

    public void RemoveBuff(StatType stat, float amount)
    {
        switch (stat)
        {
            case StatType.Health:
                //currentHealth -= amount;
                HealthBuff -= amount;
                break;
            case StatType.Stamina:
                //currentStamina -= amount;
                StaminaBuff -= amount;
                break;
            case StatType.Mana:
                //currentMana -= amount;
                ManaBuff -= amount;
                break;
        }
    }

    // public void RemoveBuffOverTime(StatType stat, float totalDamage, float time)
    // {
    //     float dmgEachTime = totalDamage / time;
    //     float totalTime = 0f;
    //     while (totalTime <= time)
    //     {
    //         
    //     }
    // }
    public void ClearAllBuffs()
    {
        HealthBuff = 0f;
        StaminaBuff = 0f;
        ManaBuff = 0f;
    }

    public void AddBuff(StatType stat, float amount)
    {
        switch (stat)
        {
            case StatType.Health:
                HealthBuff += amount;
                break;
            case StatType.Stamina:
                StaminaBuff += amount;
                break;
            case StatType.Mana:
                ManaBuff += amount;
                break;
        }
    }

    public void AddBuffOverTime()
    {
    }
}