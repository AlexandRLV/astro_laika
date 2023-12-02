using Cysharp.Threading.Tasks;
using DI;
using Missions;
using PlayerProgress;
using UnityEngine;

namespace Startup.InGame
{
    public class MissionsInitializer : InitializerBase
    {
        public override UniTask Initialize()
        {
            var scoresCounter = GameContainer.Create<LevelScoresCounter>();
            GameContainer.InGame.Register(scoresCounter);
            
            var missionsController = GameContainer.Create<MissionsController>();
            GameContainer.InGame.Register(missionsController);
            
            var mission01 = Resources.Load<MissionData>("Configs/Missions/Mission 01");
            missionsController.StartMission(mission01);
            
            return UniTask.CompletedTask;
        }
    }
}