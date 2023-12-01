using System;
using UnityEngine;

namespace Damage
{
    public class Damageable : MonoBehaviour
    {
        public event Action<DamageType> OnDestroyed;
    
        [Header("Health")]
        [SerializeField] private float _initialHealth;
    
        [Header("Shield")]
        [SerializeField] private bool _useShield;
        [SerializeField] private float _initialShield;
        [SerializeField] private float _shieldRepairPerSecond;
    
        private HealthStatus _healthStatus;

        private bool _shieldIsActive;
        private float _currentHealth;
        private float _currentShield;

        private void Awake()
        {
            _currentHealth = _initialHealth;
            _currentShield = _initialShield;
        }

        public void SetStatusCanvas(HealthStatus healthStatus)
        {
            _healthStatus = healthStatus;
            _healthStatus.HealthBarChange(_currentHealth, _initialHealth);

            _shieldIsActive = _useShield;

            if (!_useShield)
                _healthStatus.DeactivateShield();
            else
                _healthStatus.ShieldBarChange(_currentShield, _initialShield);
        }

        public void Damage(float damage, DamageType damageType)
        {
            if (_shieldIsActive)
            {
                _currentShield = Mathf.Clamp(_currentShield - damage, 0, _initialShield);
                _healthStatus.ShieldBarChange(_currentShield, _initialShield);
                if (_currentShield <= 0)
                {
                    _shieldIsActive = false;
                    _healthStatus.DeactivateShield();
                }
            }
            else
            {
                _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _initialHealth);
                _healthStatus.HealthBarChange(_currentHealth, _initialHealth);
            
                if (_currentHealth <= 0)
                    Destruct(damageType);
            }
        }

        public void RepairShield(float percent)
        {
            if (_currentShield >= _initialShield)
                return;
            
            if (percent is <= 0f or > 1)
                return;

            float repairAmount = _initialShield * percent;
            _currentShield = Mathf.Min(_currentShield + repairAmount, _initialShield);
            _healthStatus.ShieldBarChange(_currentShield, _initialShield);
        }

        private void Destruct(DamageType damageType)
        {
            OnDestroyed?.Invoke(damageType);
            Destroy(gameObject);
        }
    }
}
