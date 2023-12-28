using Cysharp.Threading.Tasks;
using DI;
using PlayerProgress;
using Services.WindowsSystem;
using Startup;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Windows
{
    public class MissionCompletedWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _scoresText;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        
        [Inject] private GameInitializer _gameInitializer;
        [Inject] private LevelScoresCounter _scoresCounter;

        private void Start()
        {
            _scoresText.text = _scoresCounter.CurrentScores.ToString();
            _restartButton.onClick.AddListener(() =>
            {
                windowsSystem.DestroyWindow(this);
                _gameInitializer.RestartCurrentGame().Forget();
            });
            _menuButton.onClick.AddListener(() =>
            {
                windowsSystem.DestroyWindow(this);
                _gameInitializer.BackToMenu().Forget();
            });
        }
    }
}