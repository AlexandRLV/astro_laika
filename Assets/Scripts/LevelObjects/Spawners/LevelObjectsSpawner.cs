using System.Collections.Generic;
using DefaultNamespace;
using DI;
using UnityEngine;

namespace LevelObjects
{
    public class LevelObjectsSpawner : ObjectsSpawnerBase
    {
        [Header("First object")]
        [SerializeField] private bool _spawnFirstObject;
        [SerializeField] private float _firstObjectYOffset;
        
        [Header("Objects spawn")]
        [SerializeField] private int _maxActiveObjects;
        [SerializeField] private Vector2 _minMaxTimeToSpawn;
        [SerializeField] private float _maxXOffset;
        [SerializeField] private float _yToDespawn;
        [SerializeField] private LevelDestroyableObject[] _spawnObjects;
        
        private float _timer;
        private bool _canSpawn;
        private int _remainingCount;
        private LevelObjectData _data;
        private List<LevelDestroyableObject> _spawnedObjects;

        public override void StartSpawn(int count, LevelObjectData data)
        {
            _remainingCount = count;
            _canSpawn = true;
            _data = data;
        }

        public override void StopSpawn()
        {
            _remainingCount = 0;
            _canSpawn = false;
        }

        private void Awake()
        {
            _spawnedObjects = new List<LevelDestroyableObject>();
            SetTimeToSpawn();
            
            if (_spawnFirstObject)
                SpawnRandomObject(_firstObjectYOffset);
        }

        private void OnDestroy()
        {
            foreach (var spawnedObject in _spawnedObjects)
            {
                Destroy(spawnedObject.gameObject);
            }
        }

        private void Update()
        {
            UpdateSpawnedObjects();
            
            if (_spawnedObjects.Count >= _maxActiveObjects || !_canSpawn || _remainingCount <= 0)
                return;
            
            _timer -= Time.deltaTime;
            if (_timer > 0f) return;
            
            SpawnRandomObject();
            SetTimeToSpawn();
        }

        private void UpdateSpawnedObjects()
        {
            for (int i = _spawnedObjects.Count - 1; i >= 0; i--)
            {
                var spawnedObject = _spawnedObjects[i];
                if (spawnedObject.transform.position.y > _yToDespawn) continue;

                spawnedObject.OnObjectDestroyed -= OnObjectDestroyed;
                Destroy(spawnedObject);
                _spawnedObjects.RemoveAt(i);
            }
        }

        private void SetTimeToSpawn()
        {
            _timer = Random.Range(_minMaxTimeToSpawn.x, _minMaxTimeToSpawn.y);
        }

        private void SpawnRandomObject(float yOffset = 0f)
        {
            var position = transform.position
                .AddY(yOffset)
                .WithX(Random.Range(-_maxXOffset, _maxXOffset));
            
            var objectToSpawn = _spawnObjects.GetRandom();
            var spawnedObject = GameContainer.InstantiateAndResolve(objectToSpawn, position, Quaternion.identity);
            
            spawnedObject.InitializeWithData(_data);
            spawnedObject.OnObjectDestroyed += OnObjectDestroyed;
            
            _spawnedObjects.Add(spawnedObject);
        }

        private void OnObjectDestroyed(LevelDestroyableObject target)
        {
            target.OnObjectDestroyed -= OnObjectDestroyed;
            _spawnedObjects.Remove(target);
            _remainingCount--;
        }
    }
}