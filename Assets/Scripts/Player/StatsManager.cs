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
        heatlhStat = stats.First(stat => stat.BaseStat.name == "Health");
        staminaStat = stats.First(stat => stat.BaseStat.name == "Stamina");
        manaStat = stats.First(stat => stat.BaseStat.name == "Mana");
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

    public void DrainStat(string stat, float amount)
    {
        switch (stat)
        {
            case "Health":
                heatlhStat.DrainStat(amount);
                break;
            case "Stamina":
                staminaStat.DrainStat(amount);
                break;
            case "Mana":
                manaStat.DrainStat(amount);
                break;
        }
    }

    public void DrainStatOverTime(string stat, float totalDrain, float totalTime)
    {
        switch (stat)
        {
            case "Health":
                heatlhStat.DrainStatOverTime(totalDrain, totalTime);
                break;
            case "Stamina":
                staminaStat.DrainStatOverTime(totalDrain, totalTime);
                break;
            case "Mana":
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

    public void AddBuff(string stat, float value)
    {
        switch (stat)
        {
            case "Health":
                AddHealthBuff(value);
                break;
            case "Stamina":
                AddStaminaBuff(value);
                break;
            case "Mana":
                AddManaBuff(value);
                break;
        }
    }

    public void RemoveBuff(string stat)
    {
        switch (stat)
        {
            case "Health":
                RemoveBuffAmount("Health", heatlhStat.Buff);
                break;
            case "Stamina":
                RemoveBuffAmount("Stamina", staminaStat.Buff);
                break;
            case "Mana":
                RemoveBuffAmount("Mana", manaStat.Buff);
                break;
        }
    }

    public void RemoveBuffAmount(string stat, float value)
    {
        switch (stat)
        {
            case "Health":
                heatlhStat.RemoveBuff(value);
                break;
            case "Stamina":
                staminaStat.RemoveBuff(value);
                break;
            case "Mana":
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