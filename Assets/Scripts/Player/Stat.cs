using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Stats;
using UnityEngine;


public enum RecoveryState
{
    CantRecover,
    Recovering,
    CanRecover
}

// potentially use a scriptable object
public class Stat : MonoBehaviour
{
   
    #region Stat Bar

    [Header("Stats Bars")] [SerializeField]
    private StatsBar statBar;

    #endregion

    #region Stat Scriptable Object

    [SerializeField] private BaseStat baseStat;
    public BaseStat BaseStat => baseStat;
    #endregion

    #region Passive Recovery
    
    private RecoveryState _currentRecoveryState;

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
        _currentRecoveryState = baseStat.DefaultRecoveryState;
        MaxValue = baseStat.BaseValue + buff;

        statBar.SetInitialValue(MaxValue);

        CurrentValue = MaxValue;
    }

    private void Update()
    {
        if (_currentRecoveryState == RecoveryState.CanRecover && currentValue < maxValue)
        {
            _currentRecoveryState = RecoveryState.Recovering;
            RecoverStatOverTime();
        }
    }

    #endregion

    #region Stats Methods

    public void DrainStat(float amount)
    {
        _currentRecoveryState = RecoveryState.CantRecover;

        CurrentValue -= amount;

        StartCoroutine(CanRecoverAfterDrain());
    }

    public void DrainStatOverTime(float totalDrain, float totalTime)
    {
        _currentRecoveryState = RecoveryState.CantRecover;
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
        yield return new WaitForSeconds(baseStat.RecoverAfterDrainTime);
        _currentRecoveryState = RecoveryState.CanRecover;
    }

    private void RecoverStatOverTime()
    {
        StartCoroutine(_RecoverStatOverTime(val => CurrentValue = val, CurrentValue, MaxValue));
    }

    private IEnumerator _RecoverStatOverTime(Action<float> callback, float stat, float maxStat)
    {
        float timeElapsed = 0f;
        float totalTime = (maxStat - stat) / baseStat.RecoveryPerSecond;

        while (timeElapsed < totalTime && _currentRecoveryState == RecoveryState.Recovering)
        {
            callback(Mathf.Lerp(stat, maxStat,
                timeElapsed / totalTime));

            timeElapsed += Time.deltaTime;

            yield return null;
        }
        
        if (_currentRecoveryState == RecoveryState.Recovering)
        {
            callback(maxStat);
            _currentRecoveryState = RecoveryState.CanRecover;
        }
        else
        {
            _currentRecoveryState = RecoveryState.CantRecover;
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