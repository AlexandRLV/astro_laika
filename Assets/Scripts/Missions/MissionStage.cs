using System;
using DI;
using LevelObjects;
using LevelObjects.Messages;
using Player;
using PlayerProgress;
using Services;
using UnityEngine;

namespace Missions
{
    [Serializable]
    public class MissionStage
    {
        [SerializeField] public LevelObjectType SpawnObjectsType;
        [SerializeField] public int ObjectsCount;
        
        [Inject] private MessageBroker _messageBroker;
        [Inject] private MissionsController _missionsController;
        [Inject] private LevelObjectsStorage _levelObjectsStorage;
        [Inject] private ObjectsSpawnerService _spawnerService;
        [Inject] private LevelScoresCounter _scoresCounter;
        [Inject] private PlayerController _player;
        
        private int _destroyedCount;
        private LevelObjectData _targetData;
        private ObjectsSpawnerBase _spawner;
        
        public void Initialize()
        {
            _destroyedCount = 0;
            GameContainer.InjectToInstance(this);
            _messageBroker.Subscribe<LevelObjectDestroyedMessage>(OnLevelObjectDestroyed);

            _targetData = _levelObjectsStorage.FindDataForType(SpawnObjectsType);
            _spawner = _spawnerService.GetSpawnerForType(SpawnObjectsType);
            _spawner.StartSpawn(ObjectsCount);
        }

        private void OnLevelObjectDestroyed(ref LevelObjectDestroyedMessage message)
        {
            if (message.Data.Type != SpawnObjectsType)
                return;

            _destroyedCount++;
            if (!message.DestroyedByCollision)
                _scoresCounter.AddScores(_targetData.ScoresForDestroy);
            
            if (_destroyedCount < ObjectsCount)
                return;
            
            _missionsController.CompleteStage(this);
        }

        public void Dispose()
        {
            _messageBroker.Unsubscribe<LevelObjectDestroyedMessage>(OnLevelObjectDestroyed);
            _spawner.StopSpawn();
        }
    }
}