using System;
using Levels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Windows.Extra
{
    public class ChooseLevelButton : MonoBehaviour
    {
        public event Action<LevelInfo> OnPressed;
        
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _completedBg;
        [SerializeField] private GameObject _notCompletedBg;

        public void Initialize(LevelInfo levelInfo, bool completed)
        {
            _levelText.text = levelInfo.LevelName;
            _button.onClick.AddListener(() => OnPressed?.Invoke(levelInfo));
            _completedBg.SetActive(completed);
            _notCompletedBg.SetActive(!completed);
        }
    }
}