using DI;
using Ui.Windows;

namespace PlayerProgress
{
    public class LevelScoresCounter
    {
        [Inject] private InGameUI _inGameUI;
        
        public int CurrentScores { get; private set; }

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