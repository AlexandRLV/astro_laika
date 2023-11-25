using Cysharp.Threading.Tasks;
using DI;
using Services.WindowsSystem;
using Startup;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Windows
{
    public class PauseWindow : WindowBase
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _menuButton;

        [Inject] private GameInitializer _gameInitializer;

        private void Start()
        {
            Time.timeScale = 0f;
            
            _continueButton.onClick.AddListener(Close);
            _settingsButton.onClick.AddListener(OpenSettings);
            _menuButton.onClick.AddListener(GoToMenu);
        }

        private void Close()
        {
            Time.timeScale = 1f;
            windowsSystem.DestroyWindow(this);
            windowsSystem.TryGetWindow(out InGameUI inGameUI);
            inGameUI.gameObject.SetActive(true);
        }

        private void OpenSettings()
        {
            windowsSystem.CreateWindow<SettingsWindow>();
        }

        private void GoToMenu()
        {
            Close();
            _gameInitializer.BackToMenu().Forget();
        }
    }
}