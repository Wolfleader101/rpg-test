using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatsManager : MonoBehaviour
{

    [SerializeField] private List<Stat> stats;
    
    #region Unity Events

    private void Start()
    {

        foreach (var stat in stats)
        {
            Debug.Log(stat.StatType);
        }
    }

    private void Update()
    {

    }

    #endregion

    // #region Stats Methods
    //
    // public void Damage(float amount)
    // {
    //     currentHealth -= amount;
    //
    //     healthBar.SetValue(currentHealth);
    //
    //     //if (currentHealth <= 0) Kill();
    // }
    //
    // public void DrainStat(StatType stat, float amount)
    // {
    //     switch (stat)
    //     {
    //         case StatType.Health:
    //             currentHealth -= amount;
    //             break;
    //         case StatType.Stamina:
    //             currentStamina -= amount;
    //             break;
    //         case StatType.Mana:
    //             currentMana -= amount;
    //             break;
    //     }
    // }
    //
    // public void DrainStatOverTime(StatType stat, float totalDrain, float totalTime)
    // {
    //     switch (stat)
    //     {
    //         case StatType.Health:
    //             StartCoroutine(_DrainStatOverTime(val => CurrentHealth = val, CurrentHealth, totalDrain, totalTime
    //             ));
    //             break;
    //         case StatType.Stamina:
    //             StartCoroutine(_DrainStatOverTime(val => CurrentStamina = val, CurrentStamina, totalDrain, totalTime
    //             ));
    //             break;
    //         case StatType.Mana:
    //             StartCoroutine(_DrainStatOverTime(val => CurrentMana = val, CurrentMana, totalDrain, totalTime
    //             ));
    //             break;
    //     }
    // }
    //
    // private IEnumerator _DrainStatOverTime(Action<float> callback, float stat, float totalDrain, float totalTime)
    // {
    //     float timeElapsed = 0f;
    //     float endStat = (stat - totalDrain);
    //
    //     while (timeElapsed < totalTime)
    //     {
    //         callback(Mathf.Lerp(stat, endStat,
    //             timeElapsed / totalTime));
    //
    //         timeElapsed += Time.deltaTime;
    //
    //         yield return null;
    //     }
    //
    //     callback(endStat);
    // }
    //
    // private void RecoverStatOverTime(StatType stat)
    // {
    //     switch (stat)
    //     {
    //         case StatType.Health:
    //             StartCoroutine(_RecoverStatOverTime(val => CurrentHealth = val, CurrentHealth, MaxHealth,
    //                 healthRecoveryPerSecond, val => canRecoverHealth = true));
    //             break;
    //         case StatType.Stamina:
    //             StartCoroutine(_RecoverStatOverTime(val => CurrentStamina = val, CurrentStamina, MaxStamina,
    //                 staminaRecoveryPerSecond, val => canRecoverStamina = true));
    //             break;
    //         case StatType.Mana:
    //             StartCoroutine(_RecoverStatOverTime(val => CurrentMana = val, CurrentMana, MaxMana,
    //                 manaRecoveryPerSecond, val => canRecoverMana = val));
    //             break;
    //     }
    // }
    //
    // private IEnumerator _RecoverStatOverTime(Action<float> callback, float stat, float maxStat, float recoveryPerSecond,
    //     Action<bool> canRecover)
    // {
    //     float timeElapsed = 0f;
    //     float totalTime = (maxStat - stat) / recoveryPerSecond;
    //
    //     while (timeElapsed < totalTime)
    //     {
    //         callback(Mathf.Lerp(stat, maxStat,
    //             timeElapsed / totalTime));
    //
    //         timeElapsed += Time.deltaTime;
    //
    //         yield return null;
    //     }
    //
    //     callback(maxStat);
    //     canRecover(true);
    // }
    //
    // #endregion
    //
    // #region Buff Methods
    //
    // private void AddHealthBuff(float value)
    // {
    //     HealthBuff += value;
    //     MaxHealth += value;
    // }
    //
    // private void AddStaminaBuff(float value)
    // {
    //     StaminaBuff += value;
    //     MaxStamina += value;
    // }
    //
    // private void AddManaBuff(float value)
    // {
    //     ManaBuff += value;
    //     MaxMana += value;
    // }
    //
    // public void AddBuff(StatType stat, float value)
    // {
    //     switch (stat)
    //     {
    //         case StatType.Health:
    //             AddHealthBuff(value);
    //             break;
    //         case StatType.Stamina:
    //             AddStaminaBuff(value);
    //             break;
    //         case StatType.Mana:
    //             AddManaBuff(value);
    //             break;
    //     }
    // }
    //
    // public void RemoveBuff(StatType stat)
    // {
    //     switch (stat)
    //     {
    //         case StatType.Health:
    //             RemoveBuff(StatType.Health, HealthBuff);
    //             break;
    //         case StatType.Stamina:
    //             RemoveBuff(StatType.Stamina, StaminaBuff);
    //             break;
    //         case StatType.Mana:
    //             RemoveBuff(StatType.Mana, ManaBuff);
    //             break;
    //     }
    // }
    //
    // public void RemoveBuff(StatType stat, float value)
    // {
    //     switch (stat)
    //     {
    //         case StatType.Health:
    //             HealthBuff -= value;
    //             maxHealth -= value;
    //             break;
    //         case StatType.Stamina:
    //             StaminaBuff -= value;
    //             MaxStamina -= value;
    //             break;
    //         case StatType.Mana:
    //             ManaBuff -= value;
    //             MaxMana -= value;
    //
    //             break;
    //     }
    // }
    //
    // public void ClearAllBuffs()
    // {
    //     RemoveBuff(StatType.Health, HealthBuff);
    //     RemoveBuff(StatType.Stamina, StaminaBuff);
    //     RemoveBuff(StatType.Mana, ManaBuff);
    // }
    //
    // #endregion
}