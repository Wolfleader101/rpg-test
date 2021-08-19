using System;
using UnityEngine;

namespace ScriptableObjects.Stats
{
    [CreateAssetMenu( menuName = "Stat")]
    public class BaseStat : ScriptableObject
    {
        #region Base Value

        [Header("Base Stats")] [SerializeField]
        private float baseValue = 100f;

        public float BaseValue => baseValue;

        #endregion

        #region Passive Recovery

        [Header("Passive Recovery")] 
        [SerializeField] private float recoveryPerSecond = 4f;

        public float RecoveryPerSecond => recoveryPerSecond;

        [SerializeField] private float recoverAfterDrainTime = 3f;

        public float RecoverAfterDrainTime => recoverAfterDrainTime;
    
        [SerializeField] RecoveryState defaultRecoveryState = RecoveryState.CanRecover;

        public RecoveryState DefaultRecoveryState => defaultRecoveryState;

        #endregion


        #region Unity Events

        private void OnEnable()
        {
            
            
        }

        private void OnDisable()
        {
            
        }

        #endregion
    }
}