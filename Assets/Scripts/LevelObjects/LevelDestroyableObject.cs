using System;
using Damage;
using DI;
using LevelObjects.Messages;
using Player;
using Services;
using UnityEngine;

namespace LevelObjects
{
    public class LevelDestroyableObject : MonoBehaviour
    {
        public event Action<LevelDestroyableObject> OnObjectDestroyed;
        public LevelObjectData Data { get; private set; }
        
        [SerializeField] private Damageable _damageable;
        [SerializeField] private HealthStatus _healthStatus;

        [Inject] private MessageBroker _messageBroker;
        
        private void Start()
        {
            _damageable.Initialize(_healthStatus);
            _damageable.OnDestroyed += OnDestroyed;
        }

        public void InitializeWithData(LevelObjectData data)
        {
            Data = data;
        }

        private void OnDestroyed(DamageType damageType)
        {
            _damageable.OnDestroyed -= OnDestroyed;
            OnObjectDestroyed?.Invoke(this);
            
            var message = new LevelObjectDestroyedMessage
            {
                DamageType = damageType,
                Data = Data,
            };
            _messageBroker.Trigger(ref message);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var player = other.collider.GetComponentInParent<PlayerController>();
            if (player == null)
                return;
            
            _damageable.Destruct(DamageType.Collision);
        }
    }
}