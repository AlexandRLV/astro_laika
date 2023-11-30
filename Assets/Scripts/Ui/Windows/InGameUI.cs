using Services.WindowsSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Windows
{
    public class InGameUI : WindowBase
    {
        public HealthStatus PlayerHealthStatus => _playerHealthStatus;
        
        [SerializeField] private Button _pauseButton;
        [SerializeField] private HealthStatus _playerHealthStatus;
        
        private void Start()
        {
            _pauseButton.onClick.AddListener(Pause);
        }

        private void Pause()
        {
            windowsSystem.CreateWindow<PauseWindow>();
            gameObject.SetActive(false);
        }
    }
}