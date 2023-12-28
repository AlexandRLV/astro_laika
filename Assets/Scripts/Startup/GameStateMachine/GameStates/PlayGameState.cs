using Cysharp.Threading.Tasks;
using DI;
using Services;
using Services.SoundsSystem;
using Ui;
using UnityEngine.SceneManagement;

namespace Startup.GameStateMachine.GameStates
{
    public class PlayGameState : IGameState
    {
        [Inject] private SoundsSystem _soundsSystem;
        [Inject] private GameInfoContainer _gameInfoContainer;
        [Inject] private LoadingScreen _loadingScreen;

        private string _loadedScene;

        public async UniTask OnEnter()
        {
            _loadingScreen.Active = true;
            
            _loadedScene = _gameInfoContainer.CurrentLevel.SceneToLoad;
            await SceneManager.LoadSceneAsync(_loadedScene, LoadSceneMode.Additive);
            _soundsSystem.PlayMusic(_gameInfoContainer.CurrentLevel.LevelMusic);
            
            _loadingScreen.Active = false;
        }

        public async UniTask OnExit()
        {
            await SceneManager.UnloadSceneAsync(_loadedScene);
        }
    }
}