using DI;
using Player;
using UnityEngine;

namespace Startup.InGame
{
    public class PlayerInitializer : LevelInitializerBase
    {
        [SerializeField] private Transform _playerSpawn;
        
        public override void Initialize()
        {
            var playerPrefab = Resources.Load<PlayerController>("Prefabs/Player");
            var player = GameContainer.InstantiateAndResolve(playerPrefab);
            GameContainer.InGame.Register(player);
            
            player.transform.position = _playerSpawn.position;
        }

        public override void Dispose()
        {
            var player = GameContainer.InGame.Resolve<PlayerController>();
            Destroy(player.gameObject);
        }
    }
}