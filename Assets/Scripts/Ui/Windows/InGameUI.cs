using Services.WindowsSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Windows
{
    public class InGameUI : WindowBase
    {
        [SerializeField] private Button _pauseButton;
        
        private void Start()
        {
            _pauseButton.onClick.AddListener(Pause);
        }

        private void Pause()
        {
            windowsSystem.CreateWindow<PauseWindow>();
            gameObject.SetActive(false);
        }
    }
}