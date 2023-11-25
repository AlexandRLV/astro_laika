using Cysharp.Threading.Tasks;
using DI;
using Services.SoundsSystem;
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

        [Inject] private SoundsSystem _soundsSystem;
        [Inject] private WindowsSystem _windowsSystem;
        
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
            
            _soundsSystem.PlayMusic(MusicType.InGame);
        }

        public async UniTask BackToMenu()
        {
            foreach (var initializer in _inGameInitializers)
            {
                await initializer.Dispose();
            }
            GameContainer.InGame = null;
            
            _soundsSystem.PlayMusic(MusicType.MainMenu);
            _windowsSystem.TryGetWindow<MainMenuWindow>(out var mainMenu);
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
            
            GameContainer.InjectToInstance(this);
            _soundsSystem.PlayMusic(MusicType.MainMenu);
        }
    }
}