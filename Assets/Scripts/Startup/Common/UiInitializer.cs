using Cysharp.Threading.Tasks;
using DI;
using Environment;
using Services.WindowsSystem;
using Ui;
using UnityEngine;

namespace Startup.Common
{
    public class UiInitializer : InitializerBase
    {
        public override UniTask Initialize()
        {
            var uiRootPrefab = Resources.Load<UiRoot>("Ui/UiRoot");
            var uiRoot = Object.Instantiate(uiRootPrefab);
            GameContainer.Common.Register(uiRoot);

            var gameWindows = Resources.Load<GameWindows>("Configs/Game Windows");
            var windowsSystem = new WindowsSystem();
            // windowsSystem.Initialize(gameWindows, uiRoot);
            GameContainer.Common.Register(windowsSystem);
            
            var loadingScreenPrefab = Resources.Load<LoadingScreen>("Ui/LoadingScreen");
            var loadingScreen = Object.Instantiate(loadingScreenPrefab, uiRoot.OverlayParent);
            loadingScreen.Active = false;
            GameContainer.Common.Register(loadingScreen);

            var backgroundChangerPrefab = Resources.Load<BackgroundChanger>("Ui/BackgroundCanvas");
            var backgroundChanger = Object.Instantiate(backgroundChangerPrefab);
            GameContainer.Common.Register(backgroundChanger);
            
            return UniTask.CompletedTask;
        }
    }
}