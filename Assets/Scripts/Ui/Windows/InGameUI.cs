using Services.WindowsSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Windows
{
    public class InGameUI : WindowBase
    {
        public HealthStatus PlayerHealthStatus => _playerHealthStatus;
        
        [SerializeField] private Button _pauseButton;
        [SerializeField] private HealthStatus _playerHealthStatus;
        [SerializeField] private TextMeshProUGUI _scoresText;
        
        private void Start()
        {
            _pauseButton.onClick.AddListener(Pause);
            UpdateScoresValue(0);
        }

        public void UpdateScoresValue(int scores)
        {
            _scoresText.text = scores.ToString();
        }

        private void Pause()
        {
            windowsSystem.CreateWindow<PauseWindow>();
            gameObject.SetActive(false);
        }
    }
}