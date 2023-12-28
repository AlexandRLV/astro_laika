using DI;
using Player;
using Services;
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
        }

        public override void Dispose()
        {
            Destroy(_player.gameObject);
        }
    }
}