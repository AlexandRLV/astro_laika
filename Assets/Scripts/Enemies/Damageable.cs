using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] float initialHealth;

    [SerializeField] float currentHealth;

    private void Awake()
    {
        currentHealth = initialHealth;
    }

    public void GetDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, initialHealth);

        if (currentHealth <= 0) Destruct();
    }

    private void Destruct()
    {
        Destroy(gameObject);
    }
}
