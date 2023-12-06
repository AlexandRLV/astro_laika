using Cysharp.Threading.Tasks;
using DI;
using Services.WindowsSystem;
using Startup;
using Ui.Windows.Extra;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Windows
{
    public class MainMenuWindow : WindowBase
    {
        [SerializeField] private MainMenuShipPreview _preview;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;

        [Inject] private GameInitializer _gameInitializer;
        
        private void Start()
        {
            _playButton.onClick.AddListener(Play);
            _settingsButton.onClick.AddListener(() =>
            {
                windowsSystem.CreateWindow<SettingsWindow>();
            });
        }

        private void OnEnable()
        {
            _preview.gameObject.SetActive(true);
            _preview.Enter();
        }

        private void Play()
        {
            _preview.Exit(() =>
            {
                _preview.gameObject.SetActive(false);
                gameObject.SetActive(false);
                _gameInitializer.StartGame().Forget();
            });
        }
    }
}