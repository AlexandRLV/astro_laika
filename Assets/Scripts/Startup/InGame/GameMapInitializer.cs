using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Startup.InGame
{
    public class GameMapInitializer : InitializerBase
    {
        private const string SceneToLoad = "Level01";
        
        public override async UniTask Initialize()
        {
            Debug.Log($"Loading scene {SceneToLoad}");
            await SceneManager.LoadSceneAsync(SceneToLoad, LoadSceneMode.Additive);
            Debug.Log("Loaded scene!");
        }

        public override async UniTask Dispose()
        {
            await SceneManager.UnloadSceneAsync(SceneToLoad);
        }
    }
}