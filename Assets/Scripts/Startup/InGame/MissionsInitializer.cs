using DI;
using LevelObjects;
using Missions;
using PlayerProgress;
using UnityEngine;

namespace Startup.InGame
{
    public class MissionsInitializer : LevelInitializerBase
    {
        [SerializeField] private LevelObjectsStorage _levelObjectsStorage;
        [SerializeField] private ObjectsSpawnerService _objectsSpawnerService;
        
        public override void Initialize()
        {
            GameContainer.InGame.Register(_levelObjectsStorage);
            GameContainer.InGame.Register(_objectsSpawnerService);
            
            _objectsSpawnerService.Initialize();
            
            var scoresCounter = GameContainer.Create<LevelScoresCounter>();
            GameContainer.InGame.Register(scoresCounter);
            
            var missionsController = GameContainer.Create<MissionsController>();
            GameContainer.InGame.Register(missionsController);
            
            var mission01 = Resources.Load<MissionData>("Configs/Missions/Mission 01");
            missionsController.StartMission(mission01);
        }
    }
}