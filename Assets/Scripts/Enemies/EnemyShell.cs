using System;
using Damage;
using Player;
using UnityEngine;

namespace Enemies
{
    public class EnemyShell : MonoBehaviour
    {
        [HideInInspector] public float DamagePercent;
        
        [SerializeField] private float _moveSpeed;
        [SerializeField] private GameObject _impactEffect;
        
        private void Update()
        {
            transform.Translate(-Vector3.up * (_moveSpeed * Time.deltaTime));   
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.attachedRigidbody.GetComponent<PlayerController>();
            if (player == null) return;
            
            var damageable = other.gameObject.GetComponentInParent<Damageable>();
            if (damageable != null)
                damageable.Damage(DamagePercent * damageable.InitialHp, DamageType.Collision);

            Instantiate(_impactEffect, other.ClosestPoint(transform.position), transform.rotation);
            Destroy(gameObject);
        }
    }
}
