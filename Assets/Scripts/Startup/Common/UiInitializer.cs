using Cysharp.Threading.Tasks;
using DI;
using PlayerShips;
using Services.WindowsSystem;
using Ui;
using Ui.Windows;
using Ui.Windows.Extra;
using UnityEngine;

namespace Startup.Common
{
    public class UiInitializer : InitializerBase
    {
        [SerializeField] private UiRoot _uiRoot;
        [SerializeField] private GameWindows _gameWindows;
        [SerializeField] private LoadingScreen _loadingScreen;
        [SerializeField] private MainMenuWindow _mainMenuWindow;
        [SerializeField] private MainMenuShipPreview _shipPreview;
        
        public override void Initialize()
        {
            GameContainer.Common.Register(_uiRoot);
            GameContainer.Common.Register(_shipPreview);
            GameContainer.Common.Register(_gameWindows);
            
            var windowsSystem = GameContainer.Create<WindowsSystem>();
            GameContainer.Common.Register(windowsSystem);
            
            _loadingScreen.Active = false;
            GameContainer.Common.Register(_loadingScreen);
            
            GameContainer.InjectToInstance(_mainMenuWindow);
            windowsSystem.AddWindow(_mainMenuWindow);
        }
    }
}