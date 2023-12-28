using DI;
using GameSettings;
using PlayerProgress;
using PlayerShips;
using Services;
using Services.SoundsSystem;
using UnityEngine;

namespace Startup.Common
{
    public class ServicesInitializer : InitializerBase
    {
        [SerializeField] private PlayerShipsData _playerShipsData;
        
        public override void Initialize()
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

            var gameInfoContainer = GameContainer.Create<GameInfoContainer>();
            GameContainer.Common.Register(gameInfoContainer);

            var progressManager = GameContainer.Create<PlayerProgressManager>();
            progressManager.LoadProgress();
            GameContainer.Common.Register(progressManager);

            if (!progressManager.Data.BoughtShips.Contains(progressManager.Data.SelectedShip) || progressManager.Data.SelectedShip >= _playerShipsData.PlayerShips.Length - 1)
                progressManager.Data.SelectedShip = 0;

            gameInfoContainer.CurrentShip = _playerShipsData.PlayerShips[progressManager.Data.SelectedShip];
            GameContainer.Common.Register(_playerShipsData);
        }
    }
}