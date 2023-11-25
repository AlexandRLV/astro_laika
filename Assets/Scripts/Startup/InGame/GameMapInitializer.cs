using Cysharp.Threading.Tasks;
using DI;
using Services.WindowsSystem;
using Ui.Windows;
using UnityEngine.SceneManagement;

namespace Startup.InGame
{
    public class GameMapInitializer : InitializerBase
    {
        private const string SceneToLoad = "Level01";
        
        public override async UniTask Initialize()
        {
            await SceneManager.LoadSceneAsync(SceneToLoad, LoadSceneMode.Additive);

            var windowsSystem = GameContainer.Common.Resolve<WindowsSystem>();
            windowsSystem.CreateWindow<InGameUI>();
        }

        public override async UniTask Dispose()
        {
            await SceneManager.UnloadSceneAsync(SceneToLoad);
        }
    }
}