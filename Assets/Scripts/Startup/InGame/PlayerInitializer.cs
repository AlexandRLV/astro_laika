using DI;
using Player;
using Services;
using Services.WindowsSystem;
using Ui.Windows;
using UnityEngine;

namespace Startup.InGame
{
    public class PlayerInitializer : InitializerBase
    {
        [SerializeField] private Transform _playerSpawn;

        private PlayerController _player;
        
        public override void Initialize()
        {
            var gameInfoContainer = GameContainer.Common.Resolve<GameInfoContainer>();
            _player = GameContainer.InstantiateAndResolve(gameInfoContainer.CurrentShip.GamePrefab);
            GameContainer.InGame.Register(_player);
            
            _player.transform.position = _playerSpawn.position;
            _player.OnDestroyed += OnPlayerDestroyed;
        }

        private void OnPlayerDestroyed()
        {
            var windowsSystem = GameContainer.Common.Resolve<WindowsSystem>();
            windowsSystem.CreateWindow<MissionFailedWindow>();
        }

        public override void Dispose()
        {
            _player.OnDestroyed -= OnPlayerDestroyed;
            Destroy(_player.gameObject);
        }
    }
}