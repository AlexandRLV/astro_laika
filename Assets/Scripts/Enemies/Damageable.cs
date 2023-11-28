using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] float _initialHealth;
    [Space]
    [SerializeField] float _initialShield;
    [SerializeField] float _shieldRepairPerSecond;
    [SerializeField] bool useShield;
    [SerializeField] bool autoRepairShield;
    [Space]
    [SerializeField] HealthStatus _healthStatus;

    bool _shieldIsActive = true;
    [SerializeField] bool _isTakingDamage = false;
    float _currentHealth;
    float _currentShield;

    public bool IsTakingDamage { get; set; }

    private void Awake()
    {

        _currentHealth = _initialHealth;
        _currentShield = _initialShield;

        _healthStatus.HealthBarChange(_currentHealth, _initialHealth);

        if (!useShield) _healthStatus.DeactivateShield();
        else _healthStatus.ShieldBarChange(_currentShield, _initialShield);
    }

    private void Update()
    {
        if (_shieldIsActive && useShield) RepairShield();
    }

    private void RepairShield()
    {
        if (_currentShield == _initialShield) return;
        Debug.Log("repairing");
        _currentShield = Mathf.Clamp(_currentShield + _shieldRepairPerSecond * Time.deltaTime, 0, _initialShield);
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
            if (_currentHealth <= 0) Destruct();
        }
    }

    private void Destruct()
    {
        Destroy(gameObject);
    }
}
