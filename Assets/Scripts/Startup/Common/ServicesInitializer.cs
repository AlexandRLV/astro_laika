using DI;
using Environment;
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
        [SerializeField] private SoundsSystem _soundsSystemPrefab;
        [SerializeField] private PlayerShipsData _playerShipsData;
        [SerializeField] private MenuBackground _menuBackground;
        
        public override void Initialize()
        {
            var messageBroker = new MessageBroker();
            GameContainer.Common.Register(messageBroker);

            var gameSettings = new GameSettingsManager();
            GameContainer.Common.Register(gameSettings);

            var soundsSystem = GameContainer.InstantiateAndResolve(_soundsSystemPrefab);
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
            
            GameContainer.Common.Register(_menuBackground);
            _menuBackground.Active = true;
        }
    }
}