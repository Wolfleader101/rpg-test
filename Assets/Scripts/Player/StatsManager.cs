using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class StatsManager : MonoBehaviour
{
    [SerializeField] private List<Stat> stats;
    public List<Stat> Stats => stats;

    [HideInInspector] public Stat heatlhStat;
    [HideInInspector] public Stat staminaStat;
    [HideInInspector] public Stat manaStat;

    #region Unity Events

    private void Start()
    {
        heatlhStat = stats.First(stat => stat.StatType == StatType.Health);
        staminaStat = stats.First(stat => stat.StatType == StatType.Stamina);
        manaStat = stats.First(stat => stat.StatType == StatType.Mana);
    }

    #endregion

    #region Stats Methods

    public void Damage(float amount)
    {
        heatlhStat.DrainStat(amount);

        if (heatlhStat.CurrentValue <= 0) Kill();
    }

    private void Kill()
    {
    }

    public void DrainStat(StatType stat, float amount)
    {
        switch (stat)
        {
            case StatType.Health:
                heatlhStat.DrainStat(amount);
                break;
            case StatType.Stamina:
                staminaStat.DrainStat(amount);
                break;
            case StatType.Mana:
                manaStat.DrainStat(amount);
                break;
        }
    }

    public void DrainStatOverTime(StatType stat, float totalDrain, float totalTime)
    {
        switch (stat)
        {
            case StatType.Health:
                heatlhStat.DrainStatOverTime(totalDrain, totalTime);
                break;
            case StatType.Stamina:
                staminaStat.DrainStatOverTime(totalDrain, totalTime);
                break;
            case StatType.Mana:
                manaStat.DrainStatOverTime(totalDrain, totalTime);
                break;
        }
    }

    #endregion

    #region Buff Methods

    public void AddHealthBuff(float value)
    {
        heatlhStat.AddBuff(value);
    }

    public void AddStaminaBuff(float value)
    {
        staminaStat.AddBuff(value);
    }

    public void AddManaBuff(float value)
    {
        manaStat.AddBuff(value);
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
                RemoveBuffAmount(StatType.Health, heatlhStat.Buff);
                break;
            case StatType.Stamina:
                RemoveBuffAmount(StatType.Stamina, staminaStat.Buff);
                break;
            case StatType.Mana:
                RemoveBuffAmount(StatType.Mana, manaStat.Buff);
                break;
        }
    }

    public void RemoveBuffAmount(StatType stat, float value)
    {
        switch (stat)
        {
            case StatType.Health:
                heatlhStat.RemoveBuff(value);
                break;
            case StatType.Stamina:
                staminaStat.RemoveBuff(value);
                break;
            case StatType.Mana:
                manaStat.RemoveBuff(value);

                break;
        }
    }

    public void ClearAllBuffs()
    {
        heatlhStat.RemoveBuff(heatlhStat.Buff);
        staminaStat.RemoveBuff(staminaStat.Buff);
        manaStat.RemoveBuff(manaStat.Buff);
    }

    #endregion
}