using DI;
using Services.WindowsSystem;
using Ui.Windows;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Damageable _playerDamageable;

        [Inject] private WindowsSystem _windowsSystem;

        private void Start()
        {
            _windowsSystem.TryGetWindow(out InGameUI window);
            _playerDamageable.SetStatusCanvas(window.PlayerHealthStatus);
        }
    }
}