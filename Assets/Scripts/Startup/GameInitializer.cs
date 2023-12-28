using Cysharp.Threading.Tasks;
using DI;
using Startup.Common;
using Startup.GameStateMachine;
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
        
        private GameStateMachine.GameStateMachine _gameStateMachine;
        
        private void Awake()
        {
            Initialize().Forget();
        }

        public async UniTask StartGame()
        {
            await _gameStateMachine.SwitchToState(GameStateType.Play);
        }

        public async UniTask BackToMenu()
        {
            GameContainer.InGame = null;
            await _gameStateMachine.SwitchToState(GameStateType.MainMenu);
        }

        private async UniTask Initialize()
        {
            GameContainer.Common = new Container();
            GameContainer.Common.Register(this);
            foreach (var initializer in _commonInitializers)
            {
                await initializer.Initialize();
            }

            _gameStateMachine = new GameStateMachine.GameStateMachine();
            await _gameStateMachine.SwitchToState(GameStateType.MainMenu, force: true);
        }
    }
}