using System;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public event Action OnDamaged;
    public event Action OnDestroyed;
    
    [Header("Health")]
    [SerializeField] private float _initialHealth;
    
    [Header("Shield")]
    [SerializeField] private bool useShield;
    [SerializeField] private bool autoRepairShield;
    [SerializeField] private float _initialShield;
    [SerializeField] private float _shieldRepairPerSecond;
    
    private HealthStatus _healthStatus;

    private bool _shieldIsActive = true;
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

        if (!useShield)
            _healthStatus.DeactivateShield();
        else
            _healthStatus.ShieldBarChange(_currentShield, _initialShield);
    }

    public void GetDamage(float damage)
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
                Destruct();
        }
    }

    private void Update()
    {
        if (_shieldIsActive && useShield)
            RepairShield();
    }

    private void RepairShield()
    {
        if (_currentShield >= _initialShield)
            return;
        
        Debug.Log("repairing");
        _currentShield = Mathf.Clamp(_currentShield + _shieldRepairPerSecond * Time.deltaTime, 0, _initialShield);
        _healthStatus.ShieldBarChange(_currentShield, _initialShield);
    }

    private void Destruct()
    {
        Destroy(gameObject);
    }
}
