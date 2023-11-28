using Cysharp.Threading.Tasks;
using DI;
using Environment;
using Player;
using UnityEngine;

namespace Startup.InGame
{
    public class PlayerInitializer : InitializerBase
    {
        public override UniTask Initialize()
        {
            var playerPrefab = Resources.Load<PlayerController>("Prefabs/Player");
            var player = GameContainer.InstantiateAndResolve(playerPrefab);
            GameContainer.InGame.Register(player);
            
            var playerPosition = Object.FindObjectOfType<PlayerSpawn>();
            player.transform.position = playerPosition.transform.position;
            
            return UniTask.CompletedTask;
        }

        public override UniTask Dispose()
        {
            var player = GameContainer.InGame.Resolve<PlayerController>();
            Object.Destroy(player.gameObject);
            
            return UniTask.CompletedTask;
        }
    }
}