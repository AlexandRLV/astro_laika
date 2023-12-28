using DI;
using PlayerProgress;
using Services;
using Services.WindowsSystem;
using Startup;
using TMPro;
using Ui.Windows.Extra;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Windows
{
    public class MainMenuWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private TextMeshProUGUI _shipNameText;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _shopButton;

        [Inject] private MainMenuShipPreview _preview;
        [Inject] private PlayerProgressManager _progressManager;
        [Inject] private GameInitializer _gameInitializer;
        [Inject] private GameInfoContainer _gameInfoContainer;
        
        private void Start()
        {
            _moneyText.text = _progressManager.Data.Money.ToString();
            _preview.SpawnShip(_gameInfoContainer.CurrentShip.PreviewPrefab);
            _shipNameText.text = _gameInfoContainer.CurrentShip.Name;
            
            _playButton.onClick.AddListener(Play);
            _settingsButton.onClick.AddListener(() =>
            {
                windowsSystem.CreateWindow<SettingsWindow>();
            });
            _shopButton.onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
                windowsSystem.CreateWindow<ChooseShipWindow>();
            });
        }

        private void OnEnable()
        {
            if (_progressManager != null && _progressManager.Data != null)
                _moneyText.text = _progressManager.Data.Money.ToString();
            
            if (_gameInfoContainer != null && _gameInfoContainer.CurrentShip != null)
            {
                _preview.SpawnShip(_gameInfoContainer.CurrentShip.PreviewPrefab);
                _shipNameText.text = _gameInfoContainer.CurrentShip.Name;
            }
            
            _preview.gameObject.SetActive(true);
            _preview.Enter();
        }

        private void Play()
        {
            _preview.Exit(() =>
            {
                _preview.gameObject.SetActive(false);
                gameObject.SetActive(false);
                windowsSystem.CreateWindow<ChooseLevelWindow>();
            });
        }
    }
}