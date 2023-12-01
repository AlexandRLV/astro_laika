using Damage;
using DI;
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

        private void Start()
        {
            _windowsSystem.TryGetWindow(out InGameUI window);
            _playerDamageable.SetStatusCanvas(window.PlayerHealthStatus);
        }

        public void RestoreLaser(float percent)
        {
            _laser.RestoreLaset(percent);
        }

        public void RestoreShield(float percent)
        {
            _playerDamageable.RepairShield(percent);
        }
    }
}