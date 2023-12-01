using Damage;
using UnityEngine;

public class EnemyShell : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _damage;

    private void Update()
    {
        transform.Translate(-transform.up * (_moveSpeed * Time.deltaTime));   
    }

    private void OnTriggerEnter(Collider collision)
    {
        var damageable = collision.gameObject.GetComponentInParent<Damageable>();
        if (damageable != null)
            damageable.Damage(_damage, DamageType.Collision);

        Destroy(gameObject);
    }
}
