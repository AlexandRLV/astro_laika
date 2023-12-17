using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DI;
using Startup.GameStateMachine.GameStates;

namespace Startup.GameStateMachine
{
    public enum GameStateType
    {
        MainMenu,
        Play
    }
    
    public class GameStateMachine
    {
        private Dictionary<GameStateType, IGameState> _states;

        private GameStateType _currentStateType;
        private IGameState _currentState;

        public GameStateMachine()
        {
            _states = new Dictionary<GameStateType, IGameState>
            {
                { GameStateType.MainMenu, GameContainer.Create<MainMenuGameState>() },
                { GameStateType.Play, GameContainer.Create<PlayGameState>() },
            };
        }

        public async UniTask SwitchToState(GameStateType stateType, bool force = false)
        {
            if (_currentStateType == stateType && !force)
                return;

            if (_currentState != null)
                await _currentState.OnExit();

            _currentStateType = stateType;
            _currentState = _states[stateType];
            await _currentState.OnEnter();
        }
    }
}