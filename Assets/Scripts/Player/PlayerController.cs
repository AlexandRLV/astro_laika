using System;
using Damage;
using DI;
using LevelObjects;
using LevelObjects.Messages;
using Services;
using Services.WindowsSystem;
using Ui.Windows;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public event Action OnDestroyed;
        
        [SerializeField] private Damageable _playerDamageable;
        [SerializeField] private LaserController _laser;

        [Inject] private WindowsSystem _windowsSystem;
        [Inject] private MessageBroker _messageBroker;

        private void Start()
        {
            _windowsSystem.TryGetWindow(out InGameUI window);
            _playerDamageable.Initialize(window.PlayerHealthStatus);
            _playerDamageable.OnDestroyed += OnPlayerDestroyed;
            _messageBroker.Subscribe<LevelObjectDestroyedMessage>(OnLevelObjectDestroyed);
        }

        private void OnPlayerDestroyed(DamageType obj)
        {
            OnDestroyed?.Invoke();
        }

        private void OnDestroy()
        {
            _messageBroker.Unsubscribe<LevelObjectDestroyedMessage>(OnLevelObjectDestroyed);
        }

        private void OnLevelObjectDestroyed(ref LevelObjectDestroyedMessage message)
        {
            if (message.DamageType == DamageType.Collision)
                return;
            
            _laser.RestoreLaset(message.Data.LaserPercentBonusForDestroy);
            _playerDamageable.RepairShield(message.Data.ShieldPercentBonusForDestroy);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var levelObject = other.collider.GetComponentInParent<LevelDestroyableObject>();
            if (levelObject == null)
                return;
            
            _playerDamageable.Damage(levelObject.Data.CollisionToPlayerDamagePercent * _playerDamageable.InitialiHp, DamageType.Collision);
        }
    }
}