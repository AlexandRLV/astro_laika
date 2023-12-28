using DI;
using LevelObjects;
using Levels;
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
            DI.GameContainer.InGame.Register(_levelObjectsStorage);
            DI.GameContainer.InGame.Register(_objectsSpawnerService);
            
            _objectsSpawnerService.Initialize();
            
            _levelScoresCounter = DI.GameContainer.Create<LevelScoresCounter>();
            DI.GameContainer.InGame.Register(_levelScoresCounter);
            
            _missionsController = DI.GameContainer.Create<MissionsController>();
            DI.GameContainer.InGame.Register(_missionsController);
            
            var mission01 = Resources.Load<MissionData>("Configs/Missions/Mission 01");
            _missionsController.StartMission(mission01);
        }

        public override void Dispose()
        {
            var progressManager = DI.GameContainer.Common.Resolve<PlayerProgressManager>();
            if (_missionsController.MissionCompleted)
            {
                var levelContainer = DI.GameContainer.Common.Resolve<GameInfoContainer>();

                if (!progressManager.Data.CompletedLevels.Contains(levelContainer.CurrentLevel.Id))
                    progressManager.Data.CompletedLevels.Add(levelContainer.CurrentLevel.Id);

            }

            progressManager.Data.Money += Mathf.Max(0, _levelScoresCounter.CurrentScores);
            progressManager.SaveProgress();
        }
    }
}