using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Health,
    Stamina,
    Magic
}
// public enum BuffType
// {
//     Health,
//     Stamina,
//     Magic
// }

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

    private float MagicBuff
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

    public void Damage(float amount)
    {
        currentHealth -= amount;
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
            case StatType.Magic:
                currentMagic -= amount;
                break;
        }
    }

    public void DrainStatOverTime(StatType stat, float totalDamage, float totalTime)
    {
        switch (stat)
        {
            case StatType.Health:
                StartCoroutine(_DrainStatOverTime(currentHealth, totalDamage, totalTime));
                break;
            case StatType.Stamina:
                StartCoroutine(_DrainStatOverTime(currentStamina, totalDamage, totalTime));
                break;
            case StatType.Magic:
                StartCoroutine(_DrainStatOverTime(CurrentMagic, totalDamage, totalTime));
                break;
        }
        
    }

    private IEnumerator _DrainStatOverTime(float stat, float totalDamage, float totalTime)
    {
        float timeElapsed = 0f;
        float endStat = (stat - totalDamage);

        while (timeElapsed < totalTime)
        {
  
            currentStamina = Mathf.Lerp(stat, endStat,
                timeElapsed / totalTime);
                
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        
        stat = endStat;
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
            case StatType.Magic:
                currentMagic -= MagicBuff;
                MagicBuff = 0f;
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
            case StatType.Magic:
                //currentMagic -= amount;
                MagicBuff -= amount;
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
        MagicBuff = 0f;
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
            case StatType.Magic:
                MagicBuff += amount;
                break;
        }
    }

    public void AddBuffOverTime()
    {
    }

    
}