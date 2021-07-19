using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "Stat", menuName = "ScriptableObjects/Stat")]
// public class BaseStat : ScriptableObject
// {
//     #region Stat Bar
//
//     [Header("Stats Bars")]
//
//     [SerializeField] private StatsBar statBar;
//     public StatsBar StatBar => statBar;
//     
//     #endregion
//
//     #region Base Value
//
//     [Header("Base Stats")] 
//     [SerializeField] private float baseValue = 100f;
//     public float BaseValue => baseValue;
//
//     #endregion
//
//     #region Passive Recovery
//
//     [Header("Passive Recovery")]
//     [SerializeField] private float recoveryPerSecond = 4f;
//
//     public bool canRecover = true;
//
//     #endregion
//
//     #region Current Stat
//
//     [Header("Current Stats")]
//     
//     private float _currentValue = 0f;
//     public float CurrentValue
//     {
//         get => _currentValue;
//         set
//         {
//             _currentValue = value;
//             statBar.SetValue(CurrentValue);
//         }
//     }
//     
//     #endregion
//
//     #region Max Stat
//
//     [Header("Max Stats")] 
//     private float _maxValue = 0f;
//     public float MaxValue
//     {
//         get => _maxValue;
//         set
//         {
//             _maxValue = value;
//             statBar.SetMaxValue(_maxValue);
//         }
//     }
//     #endregion
//
//     #region Stat Buff
//
//     [field: Header("Stat Buff")]
//     public float Buff { get; set; } = 0f;
//
//     #endregion
//
// }
