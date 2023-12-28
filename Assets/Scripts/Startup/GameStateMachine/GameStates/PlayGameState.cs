using Cysharp.Threading.Tasks;
using DI;
using Services.SoundsSystem;
using UnityEngine.SceneManagement;

namespace Startup.GameStateMachine.GameStates
{
    public class PlayGameState : IGameState
    {
        private const string SceneToLoad = "Level01";
        
        [Inject] private SoundsSystem _soundsSystem;

        public async UniTask OnEnter()
        {
            // foreach (var initializer in _initializers)
            // {
            //     await initializer.Initialize();
            // }
            
            await SceneManager.LoadSceneAsync(SceneToLoad, LoadSceneMode.Additive);
            _soundsSystem.PlayMusic(MusicType.InGame);
        }

        public async UniTask OnExit()
        {
            // foreach (var initializer in _initializers)
            // {
            //     await initializer.Dispose();
            // }
            
            await SceneManager.UnloadSceneAsync(SceneToLoad);
        }
    }
}