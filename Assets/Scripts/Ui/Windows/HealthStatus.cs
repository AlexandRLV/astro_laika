using UnityEngine;
using UnityEngine.UI;

public class HealthStatus : MonoBehaviour
{
    [SerializeField] GameObject _shieldVisuals;

    [SerializeField] Image _healthBar;
    [SerializeField] Image _shieldBar;

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
