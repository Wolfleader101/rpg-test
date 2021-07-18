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

    [Header("Stats Bars")]
    [SerializeField] private StatsBar healthBar;
    [SerializeField] private StatsBar staminaBar;
    [SerializeField] private StatsBar manaBar;

    #region Base Stats
    [Header("Base Stats")]
    [SerializeField] private float baseHealth = 100f;
    public float BaseHealth => baseHealth;

    [SerializeField] private float baseStamina = 100f;
    public float BaseStamina => baseStamina;

    [SerializeField] private float baseMana = 50f;
    public float BaseMana => baseMana;
    
    #endregion

    #region Current Stats
    
    [Header("Current Stats")]
    [SerializeField] private float currentHealth = 0f;
    public float CurrentHealth => currentHealth;

    [SerializeField] private float currentStamina = 0f;
    public float CurrentStamina => currentStamina;

    [SerializeField] private float currentMana = 0f;
    public float CurrentMana => currentMana;
    
    #endregion

    #region Max Stats
    
    [Header("Max Stats")]
    [SerializeField] private float maxHealth = 0f;
    [SerializeField] private float maxStamina = 0f;
    [SerializeField] private float maxMana = 0f;

    private float MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
            healthBar.SetMaxValue(maxHealth);
        }
    }
    
    private float MaxStamina
    {
        get => maxStamina;
        set
        {
            maxStamina = value;
            staminaBar.SetMaxValue(maxStamina);
        }
    }
    
    private float MaxMana
    {
        get => maxMana;
        set
        {
            maxMana = value;
            manaBar.SetMaxValue(maxMana);
        }
    }

    #endregion

    #region Stat Buffs

    [Header("Buff Stats")]
    [SerializeField] private float healthBuff = 0f;
    [SerializeField] private float staminaBuff = 0f;
    [SerializeField] private float manaBuff = 0f;
    public float HealthBuff { get => healthBuff; private set => healthBuff = value; }
    
    public float StaminaBuff { get => staminaBuff; private set => staminaBuff = value; }

    public float ManaBuff { get => manaBuff; private set => manaBuff = value; }
 
    #endregion

    #region Unity Events
    void Start()
    {
        MaxHealth = baseHealth + HealthBuff;
        MaxStamina = baseStamina + StaminaBuff;
        MaxMana = baseMana + ManaBuff;
        
        healthBar.SetInitialValue(MaxHealth);
        staminaBar.SetInitialValue(MaxStamina);
        manaBar.SetInitialValue(MaxMana);

        currentHealth = MaxHealth;
        currentStamina = MaxStamina;
        currentMana = maxMana;
    }
    

    #endregion

    #region Stats Methods

    public void Damage(float amount)
    {
        currentHealth -= amount;

        healthBar.SetValue(currentHealth);

        //if (currentHealth <= 0) Kill();
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

    
    #endregion
    
    #region Buff Methods
    private void AddHealthBuff(float value)
    {
        HealthBuff += value;
        MaxHealth += value;
    }
    private void AddStaminaBuff(float value)
    {
        StaminaBuff += value;
        MaxStamina += value;
    }
    private void AddManaBuff(float value)
    {
        ManaBuff += value;
        MaxMana += value;
    }
    public void AddBuff(StatType stat, float value)
    {
        switch (stat)
        {
            case StatType.Health:
                AddHealthBuff(value);
                break;
            case StatType.Stamina:
                AddStaminaBuff(value);
                break;
            case StatType.Mana:
                AddManaBuff(value);
                break;
        }
    }
    
    public void RemoveBuff(StatType stat)
    {
        switch (stat)
        {
            case StatType.Health:
                RemoveBuff(StatType.Health, HealthBuff);
                break;
            case StatType.Stamina:
                RemoveBuff(StatType.Stamina, StaminaBuff);
                break;
            case StatType.Mana:
                RemoveBuff(StatType.Mana, ManaBuff);
                break;
        }
    }

    public void RemoveBuff(StatType stat, float value)
    {
        switch (stat)
        {
            case StatType.Health:
                HealthBuff -= value;
                maxHealth -= value;
                break;
            case StatType.Stamina:
                StaminaBuff -= value;
                MaxStamina -= value;
                break;
            case StatType.Mana:
                ManaBuff -= value;
                MaxMana -= value;
                
                break;
        }
    }
    
    public void ClearAllBuffs()
    {
        RemoveBuff(StatType.Health, HealthBuff);
        RemoveBuff(StatType.Stamina, StaminaBuff);
        RemoveBuff(StatType.Mana, ManaBuff);
    }
    
    #endregion
}