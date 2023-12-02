using Damage;
using DI;
using LevelObjects.Messages;
using Services;
using Services.WindowsSystem;
using Ui.Windows;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Damageable _playerDamageable;
        [SerializeField] private LaserController _laser;

        [Inject] private WindowsSystem _windowsSystem;
        [Inject] private MessageBroker _messageBroker;

        private void Start()
        {
            _windowsSystem.TryGetWindow(out InGameUI window);
            _playerDamageable.SetStatusCanvas(window.PlayerHealthStatus);
            _messageBroker.Subscribe<LevelObjectDestroyedMessage>(OnLevelObjectDestroyed);
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
    }
}