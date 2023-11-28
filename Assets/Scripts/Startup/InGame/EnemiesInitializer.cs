using Cysharp.Threading.Tasks;
using DI;
using UnityEngine;

namespace Startup.InGame
{
    public class EnemiesInitializer : InitializerBase
    {
        public override UniTask Initialize()
        {
            var waveSpawnerPrefab = Resources.Load<EnemyShipsWaveManager>("Prefabs/WaveSpawner");
            var waveSpawner = GameContainer.InstantiateAndResolve(waveSpawnerPrefab);
            GameContainer.InGame.Register(waveSpawner);
            
            return UniTask.CompletedTask;
        }

        public override UniTask Dispose()
        {
            var waveSpawner = GameContainer.InGame.Resolve<EnemyShipsWaveManager>();
            Object.Destroy(waveSpawner.gameObject);
            
            return UniTask.CompletedTask;
        }
    }
}