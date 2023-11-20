using Cysharp.Threading.Tasks;
using DI;
using Services.WindowsSystem;
using Ui;
using Ui.Windows;
using UnityEngine;

namespace Startup.Common
{
    public class UiInitializer : InitializerBase
    {
        public override UniTask Initialize()
        {
            var uiRoot = Object.FindObjectOfType<UiRoot>();
            GameContainer.Common.Register(uiRoot);

            var gameWindows = Resources.Load<GameWindows>("Configs/Game Windows");
            var windowsSystem = new WindowsSystem();
            windowsSystem.Initialize(gameWindows, uiRoot);
            GameContainer.Common.Register(windowsSystem);
            
            var loadingScreen = Object.FindObjectOfType<LoadingScreen>(true);
            loadingScreen.Active = false;
            GameContainer.Common.Register(loadingScreen);

            var mainMenu = Object.FindObjectOfType<MainMenuWindow>();
            windowsSystem.AddWindow(mainMenu);
            
            return UniTask.CompletedTask;
        }
    }
}