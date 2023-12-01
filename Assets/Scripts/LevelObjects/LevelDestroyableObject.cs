using Damage;
using DI;
using LevelObjects.Messages;
using Services;
using UnityEngine;

namespace LevelObjects
{
    public class LevelDestroyableObject : MonoBehaviour
    {
        [SerializeField] private Damageable _damageable;
        [SerializeField] private HealthStatus _healthStatus;

        [Inject] private MessageBroker _messageBroker;

        private LevelObjectData _data;
        
        private void Start()
        {
            _damageable.SetStatusCanvas(_healthStatus);
            _damageable.OnDestroyed += OnDestroyed;
        }

        public void InitializeWithData(LevelObjectData data)
        {
            _data = data;
        }

        private void OnDestroyed(DamageType damageType)
        {
            var message = new LevelObjectDestroyedMessage
            {
                DestroyedByCollision = damageType == DamageType.Collision,
                Data = _data,
            };
            _messageBroker.Trigger(ref message);
        }
    }
}