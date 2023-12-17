using Cysharp.Threading.Tasks;
using DI;
using Services.SoundsSystem;
using Services.WindowsSystem;
using Ui.Windows;

namespace Startup.GameStateMachine.GameStates
{
    public class MainMenuGameState : IGameState
    {
        [Inject] private SoundsSystem _soundsSystem;
        [Inject] private WindowsSystem _windowsSystem;
        
        public UniTask OnEnter()
        {
            _soundsSystem.PlayMusic(MusicType.MainMenu);
            _windowsSystem.TryGetWindow<MainMenuWindow>(out var mainMenu);
            mainMenu.gameObject.SetActive(true);
            
            return UniTask.CompletedTask;
        }

        public UniTask OnExit()
        {
            return UniTask.CompletedTask;
        }
    }
}