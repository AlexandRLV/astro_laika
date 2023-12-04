using System;
using Damage;
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
        [Inject] private MonoUpdater _monoUpdater;

        private bool _isCompleted;
        private int _destroyedCount;
        private LevelObjectData _targetData;
        private ObjectsSpawnerBase _spawner;
        
        public void Initialize()
        {
            Debug.Log($"Initializing mission stage {SpawnObjectsType} with {ObjectsCount} objects");
            
            _destroyedCount = 0;
            GameContainer.InjectToInstance(this);
            _messageBroker.Subscribe<LevelObjectDestroyedMessage>(OnLevelObjectDestroyed);

            _targetData = _levelObjectsStorage.FindDataForType(SpawnObjectsType);
            _spawner = _spawnerService.GetSpawnerForType(SpawnObjectsType);
            _spawner.StartSpawn(ObjectsCount, _targetData);

            _monoUpdater.OnUpdate += OnUpdate;
        }

        private void OnUpdate()
        {
            if (_isCompleted)
                _missionsController.CompleteStage(this);
        }

        private void OnLevelObjectDestroyed(ref LevelObjectDestroyedMessage message)
        {
            if (message.Data.Type != SpawnObjectsType)
                return;

            _destroyedCount++;
            if (message.DamageType != DamageType.Collision)
                _scoresCounter.AddScores(_targetData.ScoresForDestroy);
            
            if (_destroyedCount < ObjectsCount)
                return;

            _isCompleted = true;
        }

        public void Dispose()
        {
            Debug.Log($"Disposing mission stage {SpawnObjectsType} with {ObjectsCount} objects");
            _messageBroker.Unsubscribe<LevelObjectDestroyedMessage>(OnLevelObjectDestroyed);
            _spawner.StopSpawn();
            _monoUpdater.OnUpdate -= OnUpdate;
        }
    }
}