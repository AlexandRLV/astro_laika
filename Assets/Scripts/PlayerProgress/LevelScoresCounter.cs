using DI;
using Services.WindowsSystem;
using Ui.Windows;

namespace PlayerProgress
{
    public class LevelScoresCounter
    {
        private InGameUI _inGameUI;
        
        public int CurrentScores { get; private set; }

        [Construct]
        public LevelScoresCounter(WindowsSystem _windowsSystem)
        {
            _windowsSystem.TryGetWindow(out _inGameUI);
        }

        public void AddScores(int amount)
        {
            if (amount == 0) return;
            
            CurrentScores += amount;
            _inGameUI.UpdateScoresValue(CurrentScores);
        }

        public void TakeScores(int amount)
        {
            if (CurrentScores == 0 || amount == 0) return;
            
            CurrentScores -= amount;
            if (CurrentScores < 0) CurrentScores = 0;
            
            _inGameUI.UpdateScoresValue(CurrentScores);
        }
    }
}