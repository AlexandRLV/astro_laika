using UnityEngine;
using UnityEngine.UI;

public class HealthStatus : MonoBehaviour
{
    [SerializeField] private GameObject _shieldVisuals;

    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _shieldBar;

    // for health
    public void HealthBarChange(float currentHealth, float maxHealth)
    {
        _healthBar.fillAmount = currentHealth / maxHealth;
    }

    // for shield
    public void ShieldBarChange(float currentValue, float maxValue)
    {
        _shieldBar.fillAmount = currentValue / maxValue;
    }

    public void DeactivateShield()
    {
        _shieldVisuals.SetActive(false);
    }

}
