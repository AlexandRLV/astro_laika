using Cysharp.Threading.Tasks;
using DI;
using LevelObjects;
using Missions;
using Services.WindowsSystem;
using Ui.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Startup.InGame
{
    public class GameMapInitializer : InitializerBase
    {
        private const string SceneToLoad = "Level01";
        
        public override async UniTask Initialize()
        {
            var levelObjectsStorage = Resources.Load<LevelObjectsStorage>("Configs/Level Objects Storage");
            GameContainer.InGame.Register(levelObjectsStorage);
            
            await SceneManager.LoadSceneAsync(SceneToLoad, LoadSceneMode.Additive);

            var windowsSystem = GameContainer.Common.Resolve<WindowsSystem>();
            windowsSystem.CreateWindow<InGameUI>();
            
            var spawnerService = Object.FindObjectOfType<ObjectsSpawnerService>();
            GameContainer.InGame.Register(spawnerService);
        }

        public override async UniTask Dispose()
        {
            var windowsSystem = GameContainer.Common.Resolve<WindowsSystem>();
            windowsSystem.DestroyWindow<InGameUI>();
            
            await SceneManager.UnloadSceneAsync(SceneToLoad);
        }
    }
}