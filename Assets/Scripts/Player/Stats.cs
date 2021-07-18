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
    #region Stats Bars

    [Header("Stats Bars")] [SerializeField]
    private StatsBar healthBar;

    [SerializeField] private StatsBar staminaBar;
    [SerializeField] private StatsBar manaBar;

    #endregion

    #region Base Stats

    [Header("Base Stats")] [SerializeField]
    private float baseHealth = 100f;

    public float BaseHealth => baseHealth;

    [SerializeField] private float baseStamina = 100f;
    public float BaseStamina => baseStamina;

    [SerializeField] private float baseMana = 50f;
    public float BaseMana => baseMana;

    #endregion

    #region Passive Recovery

    [SerializeField] private float healthRecoveryPerSecond = 1.5f;
    [SerializeField] private float staminaRecoveryPerSecond = 2f;
    [SerializeField] private float manaRecoveryPerSecond = 2f;

    public bool canRecoverHealth = true;
    public bool canRecoverStamina = true;
    public bool canRecoverMana = true;

    #endregion

    #region Current Stats

    [Header("Current Stats")] [SerializeField]
    private float currentHealth = 0f;

    public float CurrentHealth
    {
        get => currentHealth;
        private set
        {
            currentHealth = value;
            healthBar.SetValue(currentHealth);
        }
    }

    [SerializeField] private float currentStamina = 0f;

    public float CurrentStamina
    {
        get => currentStamina;
        private set
        {
            currentStamina = value;
            staminaBar.SetValue(currentStamina);
        }
    }

    [SerializeField] private float currentMana = 0f;

    public float CurrentMana
    {
        get => currentMana;
        private set
        {
            currentMana = value;
            manaBar.SetValue(currentMana);
        }
    }

    #endregion

    #region Max Stats

    [Header("Max Stats")] [SerializeField] private float maxHealth = 0f;
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

    [Header("Buff Stats")] [SerializeField]
    private float healthBuff = 0f;

    [SerializeField] private float staminaBuff = 0f;
    [SerializeField] private float manaBuff = 0f;

    public float HealthBuff
    {
        get => healthBuff;
        private set => healthBuff = value;
    }

    public float StaminaBuff
    {
        get => staminaBuff;
        private set => staminaBuff = value;
    }

    public float ManaBuff
    {
        get => manaBuff;
        private set => manaBuff = value;
    }

    #endregion

    #region Unity Events

    private void Start()
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

    private void Update()
    {
        if (canRecoverHealth && CurrentHealth < MaxHealth)
        {
            canRecoverHealth = false;
            RecoverStatOverTime(StatType.Health);
        }

        if (canRecoverStamina && CurrentStamina < MaxStamina)
        {
            canRecoverStamina = false;
            RecoverStatOverTime(StatType.Stamina);
        }

        if (canRecoverMana && CurrentMana < MaxMana)
        {
            canRecoverMana = false;
            RecoverStatOverTime(StatType.Mana);
        }
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

    public void DrainStatOverTime(StatType stat, float totalDrain, float totalTime)
    {
        switch (stat)
        {
            case StatType.Health:
                StartCoroutine(_DrainStatOverTime(val => CurrentHealth = val, CurrentHealth, totalDrain, totalTime
                ));
                break;
            case StatType.Stamina:
                StartCoroutine(_DrainStatOverTime(val => CurrentStamina = val, CurrentStamina, totalDrain, totalTime
                ));
                break;
            case StatType.Mana:
                StartCoroutine(_DrainStatOverTime(val => CurrentMana = val, CurrentMana, totalDrain, totalTime
                ));
                break;
        }
    }

    private IEnumerator _DrainStatOverTime(Action<float> callback, float stat, float totalDrain, float totalTime)
    {
        float timeElapsed = 0f;
        float endStat = (stat - totalDrain);

        while (timeElapsed < totalTime)
        {
            callback(Mathf.Lerp(stat, endStat,
                timeElapsed / totalTime));

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        callback(endStat);
    }

    private void RecoverStatOverTime(StatType stat)
    {
        switch (stat)
        {
            case StatType.Health:
                StartCoroutine(_RecoverStatOverTime(val => CurrentHealth = val, CurrentHealth, MaxHealth,
                    healthRecoveryPerSecond, val => canRecoverHealth = true));
                break;
            case StatType.Stamina:
                StartCoroutine(_RecoverStatOverTime(val => CurrentStamina = val, CurrentStamina, MaxStamina,
                    staminaRecoveryPerSecond, val => canRecoverStamina = true));
                break;
            case StatType.Mana:
                StartCoroutine(_RecoverStatOverTime(val => CurrentMana = val, CurrentMana, MaxMana,
                    manaRecoveryPerSecond, val => canRecoverMana = val));
                break;
        }
    }

    private IEnumerator _RecoverStatOverTime(Action<float> callback, float stat, float maxStat, float recoveryPerSecond,
        Action<bool> canRecover)
    {
        float timeElapsed = 0f;
        float totalTime = (maxStat - stat) / recoveryPerSecond;

        while (timeElapsed < totalTime)
        {
            callback(Mathf.Lerp(stat, maxStat,
                timeElapsed / totalTime));

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        callback(maxStat);
        canRecover(true);
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