using Cysharp.Threading.Tasks;
using DI;
using Services.WindowsSystem;
using Startup.Common;
using Startup.InGame;
using Ui.Windows;
using UnityEngine;

namespace Startup
{
    public class GameInitializer : MonoBehaviour
    {
        private readonly InitializerBase[] _commonInitializers =
        {
            new ServicesInitializer(),
            new UiInitializer(),
        };

        private readonly InitializerBase[] _inGameInitializers =
        {
            new GameMapInitializer(),
        };
        
        private void Awake()
        {
            Initialize().Forget();
        }

        public async UniTask StartGame()
        {
            GameContainer.InGame = new Container();
            foreach (var initializer in _inGameInitializers)
            {
                await initializer.Initialize();
            }
        }

        public async UniTask BackToMenu()
        {
            foreach (var initializer in _inGameInitializers)
            {
                await initializer.Dispose();
            }
            GameContainer.InGame = null;

            var windowsSystem = GameContainer.Common.Resolve<WindowsSystem>();
            windowsSystem.TryGetWindow<MainMenuWindow>(out var mainMenu);
            mainMenu.gameObject.SetActive(true);
        }

        private async UniTask Initialize()
        {
            GameContainer.Common = new Container();
            GameContainer.Common.Register(this);
            foreach (var initializer in _commonInitializers)
            {
                await initializer.Initialize();
            }
        }
    }
}