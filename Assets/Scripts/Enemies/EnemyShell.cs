using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShell : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _damage;

    private void Update()
    {
        transform.Translate(-transform.up * _moveSpeed * Time.deltaTime);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var damageable = collision.gameObject.GetComponentInParent<Damageable>();
        if (damageable != null)
            damageable.GetDamage(_damage);

        Destroy(gameObject);
    }
}
