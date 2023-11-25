using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace LevelObjects
{
    public class LevelObjectsSpawner : MonoBehaviour
    {
        [Header("First object")]
        [SerializeField] private bool _spawnFirstObject;
        [SerializeField] private float _firstObjectYOffset;
        
        [Header("Objects spawn")]
        [SerializeField] private int _maxActiveObjects;
        [SerializeField] private Vector2 _minMaxTimeToSpawn;
        [SerializeField] private float _maxXOffset;
        [SerializeField] private float _yToDespawn;
        [SerializeField] private GameObject[] _spawnObjects;

        private float _timer;
        private List<GameObject> _spawnedObjects;

        private void Awake()
        {
            _spawnedObjects = new List<GameObject>();
            SetTimeToSpawn();
            
            if (_spawnFirstObject)
                SpawnRandomObject(_firstObjectYOffset);
        }

        private void OnDestroy()
        {
            foreach (var spawnedObject in _spawnedObjects)
            {
                Destroy(spawnedObject);
            }
        }

        private void Update()
        {
            UpdateSpawnedObjects();
            
            if (_spawnedObjects.Count >= _maxActiveObjects)
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
            var objectToSpawn = _spawnObjects.GetRandom();
            var spawnedObject = Instantiate(objectToSpawn);
            spawnedObject.transform.position = transform.position
                .AddY(yOffset)
                .WithX(Random.Range(-_maxXOffset, _maxXOffset));
            
            _spawnedObjects.Add(spawnedObject);
        }
    }
}