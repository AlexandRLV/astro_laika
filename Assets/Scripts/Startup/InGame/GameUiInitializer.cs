using DI;
using Environment;
using Services.WindowsSystem;
using Ui.Windows;

namespace Startup.InGame
{
    public class GameUiInitializer : InitializerBase
    {
        public override void Initialize()
        {
            var menuBackground = GameContainer.Common.Resolve<MenuBackground>();
            menuBackground.Active = false;
            
            var windowsSystem = GameContainer.Common.Resolve<WindowsSystem>();
            windowsSystem.CreateWindow<InGameUI>();
        }

        public override void Dispose()
        {
            var menuBackground = GameContainer.Common.Resolve<MenuBackground>();
            menuBackground.Active = true;
            
            var windowsSystem = GameContainer.Common.Resolve<WindowsSystem>();
            windowsSystem.DestroyWindow<InGameUI>();
        }
    }
}