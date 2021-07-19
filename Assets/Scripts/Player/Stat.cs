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

// potentially use a scriptable object
public class Stat : MonoBehaviour
{
    [SerializeField] private StatType statType;
    public StatType StatType => statType;
    
    #region Stat Bar

    [Header("Stats Bars")]

    [SerializeField] private StatsBar statBar;

    #endregion

    #region Base Value

    [Header("Base Stats")] 
    [SerializeField] private float baseValue = 100f;
    public float BaseValue => baseValue;

    #endregion

    #region Passive Recovery

    [Header("Passive Recovery")]
    [SerializeField] private float recoveryPerSecond = 4f;

    public bool canRecover = true;

    #endregion

    #region Current Stat

    [Header("Current Stats")]
    
    [SerializeField] private float currentValue = 0f;
    public float CurrentValue
    {
        get => currentValue;
        private set
        {
            currentValue = value;
            statBar.SetValue(CurrentValue);
        }
    }
    
    #endregion

    #region Max Stat

    [Header("Max Stats")] 
    [SerializeField] private float maxValue = 0f;

    private float MaxValue
    {
        get => maxValue;
        set
        {
            maxValue = value;
            statBar.SetMaxValue(maxValue);
        }
    }
    #endregion

    #region Stat Buff

    [Header("Stat Buff")]
    [SerializeField] private float statBuff = 0f;

    public float StatBuff
    {
        get => statBuff;
        private set => statBuff = value;
    }
    
    #endregion

    #region Unity Events

    private void Start()
    {
        MaxValue = baseValue + statBuff;

        statBar.SetInitialValue(MaxValue);

        CurrentValue = MaxValue;
    }

    private void Update()
    {
        if (canRecover && currentValue < maxValue)
        {
            canRecover = false;
            RecoverStatOverTime();
        }
    }

    #endregion

    #region Stats Methods
    
    public void DrainStat(float amount)
    {
        CurrentValue -= amount;
    }

    public void DrainStatOverTime(float totalDrain, float totalTime)
    {
        StartCoroutine(DrainStatOverTime(val => CurrentValue = val, CurrentValue, totalDrain, totalTime
        ));
    }

    private IEnumerator DrainStatOverTime(Action<float> callback, float stat, float totalDrain, float totalTime)
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

    private void RecoverStatOverTime()
    {
        StartCoroutine(_RecoverStatOverTime(val => CurrentValue = val, CurrentValue, MaxValue,
            recoveryPerSecond, val => canRecover = true));
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

    public void AddBuff(float value)
    {
        StatBuff += value;
        MaxValue += value;
        
    }

    public void RemoveBuff()
    {
        RemoveBuff(StatBuff);
    }

    public void RemoveBuff(float value)
    {                
        StatBuff -= value;
        MaxValue -= value;

    }

    #endregion
}