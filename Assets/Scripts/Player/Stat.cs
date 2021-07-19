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

public enum RecoveryState
{
    CantRecover,
    Recovering,
    CanRecover
}

// potentially use a scriptable object
public class Stat : MonoBehaviour
{
    [SerializeField] private StatType statType;
    public StatType StatType => statType;

    #region Stat Bar

    [Header("Stats Bars")] [SerializeField]
    private StatsBar statBar;

    #endregion

    #region Base Value

    [Header("Base Stats")] [SerializeField]
    private float baseValue = 100f;

    public float BaseValue => baseValue;

    #endregion

    #region Passive Recovery

    // TODO fix up passive recovery system, right now it is broken

    [Header("Passive Recovery")] [SerializeField]
    private float recoveryPerSecond = 4f;

    [SerializeField] private float recoverAfterDrainTime = 3f;
    
    [SerializeField] RecoveryState recoveryState = RecoveryState.CanRecover;

    #endregion

    #region Current Stat

    [Header("Current Stats")] [SerializeField]
    private float currentValue = 0f;

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

    [Header("Max Stats")] [SerializeField] private float maxValue = 0f;

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

    [Header("Stat Buff")] [SerializeField] private float buff = 0f;

    public float Buff
    {
        get => buff;
        private set => buff = value;
    }

    #endregion

    #region Unity Events

    private void Start()
    {
        MaxValue = baseValue + buff;

        statBar.SetInitialValue(MaxValue);

        CurrentValue = MaxValue;
    }

    private void Update()
    {
        if (recoveryState == RecoveryState.CanRecover && currentValue < maxValue)
        {
            recoveryState = RecoveryState.Recovering;
            RecoverStatOverTime();
        }
    }

    #endregion

    #region Stats Methods

    public void DrainStat(float amount)
    {
        recoveryState = RecoveryState.CantRecover;

        CurrentValue -= amount;

        StartCoroutine(CanRecoverAfterDrain());
    }

    public void DrainStatOverTime(float totalDrain, float totalTime)
    {
        recoveryState = RecoveryState.CantRecover;
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

        StartCoroutine(CanRecoverAfterDrain());
    }

    private IEnumerator CanRecoverAfterDrain()
    {
        yield return new WaitForSeconds(recoverAfterDrainTime);
        recoveryState = RecoveryState.CanRecover;
    }

    private void RecoverStatOverTime()
    {
        StartCoroutine(_RecoverStatOverTime(val => CurrentValue = val, CurrentValue, MaxValue));
    }

    private IEnumerator _RecoverStatOverTime(Action<float> callback, float stat, float maxStat)
    {
        float timeElapsed = 0f;
        float totalTime = (maxStat - stat) / recoveryPerSecond;

        while (timeElapsed < totalTime && recoveryState == RecoveryState.Recovering)
        {
            callback(Mathf.Lerp(stat, maxStat,
                timeElapsed / totalTime));

            timeElapsed += Time.deltaTime;

            yield return null;
        }
        
        if (recoveryState == RecoveryState.Recovering)
        {
            callback(maxStat);
            recoveryState = RecoveryState.CanRecover;
        }
        else
        {
            recoveryState = RecoveryState.CantRecover;
        }
    }

    #endregion

    #region Buff Methods

    public void AddBuff(float value)
    {
        Buff += value;
        MaxValue += value;
    }

    public void RemoveBuff()
    {
        RemoveBuff(Buff);
    }

    public void RemoveBuff(float value)
    {
        Buff -= value;
        MaxValue -= value;
    }

    #endregion
}