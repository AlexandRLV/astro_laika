using Cysharp.Threading.Tasks;
using DI;
using GameSettings;
using Services;
using Services.SoundsSystem;
using UnityEngine;

namespace Startup.Common
{
    public class ServicesInitializer : InitializerBase
    {
        public override UniTask Initialize()
        {
            var messageBroker = new MessageBroker();
            GameContainer.Common.Register(messageBroker);

            var gameSettings = new GameSettingsManager();
            GameContainer.Common.Register(gameSettings);

            var soundsSystemPrefab = Resources.Load<SoundsSystem>("Services/SoundsSystem");
            var soundsSystem = GameContainer.InstantiateAndResolve(soundsSystemPrefab);
            GameContainer.Common.Register(soundsSystem);

            var monoUpdaterGO = new GameObject("Mono Updater");
            var monoUpdater = monoUpdaterGO.AddComponent<MonoUpdater>();
            GameContainer.Common.Register(monoUpdater);
            
            return UniTask.CompletedTask;
        }
    }
}