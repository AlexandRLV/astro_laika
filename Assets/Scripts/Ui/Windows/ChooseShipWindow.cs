using DI;
using PlayerProgress;
using PlayerShips;
using Services;
using Services.WindowsSystem;
using TMPro;
using Ui.Windows.Extra;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Windows
{
    public class ChooseShipWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _moneyText;
        [SerializeField] private TextMeshProUGUI _shipNameText;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _prevButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private TextMeshProUGUI _buyCostText;
        [SerializeField] private GameObject _canBuyIndicator;
        [SerializeField] private GameObject _cannotBuyIndicator;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _settingsButton;

        [Inject] private MainMenuShipPreview _preview;
        [Inject] private PlayerProgressManager _progressManager;
        [Inject] private GameInfoContainer _gameInfoContainer;
        [Inject] private PlayerShipsData _playerShipsData;

        private int _currentViewingShip;
        
        private void Start()
        {
            _moneyText.text = _progressManager.Data.Money.ToString();
            _preview.SpawnShip(_gameInfoContainer.CurrentShip.PreviewPrefab);
            _shipNameText.text = _gameInfoContainer.CurrentShip.Name;
            _preview.Enter();
            
            _menuButton.onClick.AddListener(() =>
            {
                if (_progressManager.Data.BoughtShips.Contains(_currentViewingShip))
                {
                    _progressManager.Data.SelectedShip = _currentViewingShip;
                    _progressManager.SaveProgress();
                    _gameInfoContainer.CurrentShip = _playerShipsData.PlayerShips[_currentViewingShip];
                }
                
                windowsSystem.DestroyWindow(this);
                windowsSystem.TryGetWindow<MainMenuWindow>(out var mainMenu);
                mainMenu.gameObject.SetActive(true);
            });
            
            _settingsButton.onClick.AddListener(() =>
            {
                windowsSystem.CreateWindow<SettingsWindow>();
            });
            
            _buyButton.onClick.AddListener(Buy);
            _nextButton.onClick.AddListener(() =>
            {
                _currentViewingShip++;
                _currentViewingShip %= _playerShipsData.PlayerShips.Length;
                ShowShip(_currentViewingShip);
            });
            
            _prevButton.onClick.AddListener(() =>
            {
                _currentViewingShip--;
                if (_currentViewingShip < 0) _currentViewingShip += _playerShipsData.PlayerShips.Length;
                ShowShip(_currentViewingShip);
            });
            
            ShowShip(_progressManager.Data.SelectedShip);
        }

        private void Buy()
        {
            var ship = _playerShipsData.PlayerShips[_currentViewingShip];
            if (ship.Cost > _progressManager.Data.Money) return;

            _progressManager.Data.BoughtShips.Add(_currentViewingShip);
            _progressManager.Data.Money -= ship.Cost;
            _progressManager.SaveProgress();
            
            ShowShip(_currentViewingShip);
        }

        private void ShowShip(int id)
        {
            _currentViewingShip = id;
            var ship = _playerShipsData.PlayerShips[id];
            _preview.SpawnShip(ship.PreviewPrefab);

            _shipNameText.text = ship.Name;

            if (_progressManager.Data.BoughtShips.Contains(id))
            {
                _buyButton.gameObject.SetActive(false);
                _cannotBuyIndicator.SetActive(false);
                return;
            }
            
            _buyButton.gameObject.SetActive(true);
            _buyCostText.text = ship.Cost.ToString();
            _canBuyIndicator.SetActive(ship.Cost <= _progressManager.Data.Money);
            _cannotBuyIndicator.SetActive(ship.Cost > _progressManager.Data.Money);
        }
    }
}