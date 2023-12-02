using System.Collections.Generic;
using UnityEngine;

namespace Missions
{
    public class MissionsController
    {
        private MissionData _data;
        private MissionStage _currentStage;
        private Queue<MissionStage> _stages;
        
        public void StartMission(MissionData data)
        {
            Debug.Log("Starting mission!");
            
            _stages = new Queue<MissionStage>();
            foreach (var stage in data.Stages)
            {
                _stages.Enqueue(stage);
            }
            
            MoveToNextStage();
        }
        
        public void CompleteStage(MissionStage stage)
        {
            if (stage == null)
            {
                Debug.LogError("Trying to complete null stage");
                return;
            }
            if (_currentStage == null)
            {
                Debug.LogError("Trying to complete stage, but current not started");
                return;
            }
            if (stage != _currentStage)
            {
                Debug.LogError("Trying to complete not current stage");
                return;
            }
            
            _currentStage.Dispose();
            _currentStage = null;
            MoveToNextStage();
        }
        
        private void MoveToNextStage()
        {
            if (_currentStage != null)
            {
                Debug.LogError("Trying to start next mission stage, while prev not finished!");
                return;
            }
            
            if (!_stages.TryDequeue(out var stage))
            {
                CompleteMission();
                return;
            }

            _currentStage = stage;
            _currentStage.Initialize();
        }

        private void CompleteMission()
        {
            
        }
    }
}