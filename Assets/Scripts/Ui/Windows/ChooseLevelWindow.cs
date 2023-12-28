using Cysharp.Threading.Tasks;
using DI;
using Levels;
using PlayerProgress;
using Services;
using Services.WindowsSystem;
using Startup;
using Ui.Windows.Extra;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Windows
{
    public class ChooseLevelWindow : WindowBase
    {
        [SerializeField] private GameLevelsData _levelsData;
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _settingsButton;
        
        [SerializeField] private ChooseLevelButton _buttonPrefab;
        [SerializeField] private Transform _buttonsParent;
        
        [Inject] private GameInitializer _gameInitializer;
        [Inject] private GameInfoContainer _gameInfoContainer;
        [Inject] private PlayerProgressManager _progressManager;
        
        private void Start()
        {
            foreach (var level in _levelsData.Levels)
            {
                var button = Instantiate(_buttonPrefab, _buttonsParent);
                button.Initialize(level, _progressManager.Data.CompletedLevels.Contains(level.Id));
                button.OnPressed += SelectLevel;
            }

            _menuButton.onClick.AddListener(() =>
            {
                windowsSystem.DestroyWindow(this);
                windowsSystem.TryGetWindow<MainMenuWindow>(out var mainMenu);
                mainMenu.gameObject.SetActive(true);
            });
            
            _settingsButton.onClick.AddListener(() =>
            {
                windowsSystem.CreateWindow<SettingsWindow>();
            });
        }

        private void SelectLevel(LevelInfo levelInfo)
        {
            windowsSystem.DestroyWindow(this);
            _gameInfoContainer.CurrentLevel = levelInfo;
            _gameInitializer.StartGame().Forget();
        }
    }
}