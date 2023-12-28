using DI;
using LevelObjects;
using Services.WindowsSystem;
using Ui.Windows;
using UnityEngine;

namespace Startup.InGame
{
    public class GameUiInitializer : LevelInitializerBase
    {
        public override void Initialize()
        {
            var windowsSystem = GameContainer.Common.Resolve<WindowsSystem>();
            windowsSystem.CreateWindow<InGameUI>();
        }

        public override void Dispose()
        {
            var windowsSystem = GameContainer.Common.Resolve<WindowsSystem>();
            windowsSystem.DestroyWindow<InGameUI>();
        }
    }
}