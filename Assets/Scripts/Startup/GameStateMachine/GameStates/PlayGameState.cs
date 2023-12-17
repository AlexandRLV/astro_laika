using Cysharp.Threading.Tasks;
using DI;
using Services.SoundsSystem;
using Startup.InGame;

namespace Startup.GameStateMachine.GameStates
{
    public class PlayGameState : IGameState
    {
        private readonly InitializerBase[] _initializers =
        {
            new GameMapInitializer(),
            new PlayerInitializer(),
            new MissionsInitializer(),
        };
        
        [Inject] private SoundsSystem _soundsSystem;

        public async UniTask OnEnter()
        {
            foreach (var initializer in _initializers)
            {
                await initializer.Initialize();
            }
            
            _soundsSystem.PlayMusic(MusicType.InGame);
        }

        public async UniTask OnExit()
        {
            foreach (var initializer in _initializers)
            {
                await initializer.Dispose();
            }
        }
    }
}