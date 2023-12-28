using Cysharp.Threading.Tasks;
using DI;
using Startup.GameStateMachine;
using UnityEngine;
using GameContainer = DI.GameContainer;

namespace Startup
{
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField] private InitializerBase[] _commonInitializers;
        
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
                initializer.Initialize();
            }
            
            _gameStateMachine = new GameStateMachine.GameStateMachine();
            await _gameStateMachine.SwitchToState(GameStateType.MainMenu, force: true);
        }
    }
}