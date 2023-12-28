using DI;
using LevelObjects;
using Missions;
using PlayerProgress;
using Services;
using UnityEngine;

namespace Startup.InGame
{
    public class MissionsInitializer : InitializerBase
    {
        [SerializeField] private LevelObjectsStorage _levelObjectsStorage;
        [SerializeField] private ObjectsSpawnerService _objectsSpawnerService;

        private LevelScoresCounter _levelScoresCounter;
        private MissionsController _missionsController;
        
        public override void Initialize()
        {
            GameContainer.InGame.Register(_levelObjectsStorage);
            GameContainer.InGame.Register(_objectsSpawnerService);
            
            _objectsSpawnerService.Initialize();
            
            _levelScoresCounter = GameContainer.Create<LevelScoresCounter>();
            GameContainer.InGame.Register(_levelScoresCounter);
            
            _missionsController = GameContainer.Create<MissionsController>();
            GameContainer.InGame.Register(_missionsController);
            
            var mission01 = Resources.Load<MissionData>("Configs/Missions/Mission 01");
            _missionsController.StartMission(mission01);
        }

        public override void Dispose()
        {
            var progressManager = GameContainer.Common.Resolve<PlayerProgressManager>();
            progressManager.Data.Money += Mathf.Max(0, _levelScoresCounter.CurrentScores);
            progressManager.SaveProgress();
        }
    }
}