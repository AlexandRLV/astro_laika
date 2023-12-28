using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LevelObjects;
using UnityEngine;

public class EnemyShipsWaveManager : ObjectsSpawnerBase
{
    private Vector3 EnemyMoveZoneCenter => transform.position + _enemyMoveZoneOffset;
    private Vector3 EnemyMoveZoneSize => new(_enemyMoveZoneWidth, _enemyMoveZoneHeight, 0f);
    
    [Header("Timers")]
    [SerializeField] private float _startDelay;
    [SerializeField] private float _timeBetweenWaves;
    [SerializeField] private bool _waitForLastEnemy;
    
    [Header("Spawn zones")]
    [SerializeField, Range(1, 10)] private float _enemyMoveZoneWidth;
    [SerializeField, Range(1, 10)] private float _enemyMoveZoneHeight;
    [SerializeField] private Vector3 _enemyMoveZoneOffset;
    [SerializeField] private Transform[] _spawnPoints;
    
    [Header("Waves")]
    [SerializeField] private Wave[] allWaves;

    private bool _canSpawn;
    private int _remainingCount;
    
    private float _countdownToNewWave;
    private int _waveIndex;
    private int _enemiesAlive;
    
    private LevelObjectData _enemyData;
    private List<EnemyMoveController> _spawnedEnemies;

    public override void Initialize()
    {
        _spawnedEnemies = new List<EnemyMoveController>();
    }
    
    public override void StartSpawn(int count, LevelObjectData data)
    {
        _countdownToNewWave = _startDelay;
        _enemyData = data;
        _canSpawn = true;
        _remainingCount = count;
    }

    public override void StopSpawn()
    {
        _canSpawn = false;
        _remainingCount = 0;
    }

    private void Update()
    {
        if (!_canSpawn || _remainingCount <= 0)
            return;
        
        if (_waitForLastEnemy && _enemiesAlive > 0)
            return;

        if (_countdownToNewWave <= 0 && _waveIndex < allWaves.Length)
        {
            if (allWaves[_waveIndex].totalEnemiesInWave <= 0)
                return;
            
            StartCoroutine(SpawnWave(_waveIndex));
            _countdownToNewWave = _timeBetweenWaves;
        }

        _countdownToNewWave -= Time.deltaTime;
        _countdownToNewWave = Mathf.Clamp(_countdownToNewWave, 0, _timeBetweenWaves);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        if (_spawnedEnemies is not { Count: > 0 })
            return;
        
        foreach (var spawnedEnemy in _spawnedEnemies)
        {
            Destroy(spawnedEnemy.gameObject);
        }

        _spawnedEnemies.Clear();
    }

    private void SpawnEnemy(EnemyMoveController enemy)
    {
        int i = Mathf.RoundToInt(Random.Range(0, _spawnPoints.Length));
        var newEnemy = Instantiate(enemy, _spawnPoints[i].position, enemy.transform.rotation);
        newEnemy.Init(EnemyMoveZoneCenter, EnemyMoveZoneSize, _enemyData);
        _spawnedEnemies.Add(newEnemy);
        _enemiesAlive++;
        
        var enemyLevelObject = newEnemy.GetComponent<LevelDestroyableObject>();
        enemyLevelObject.InitializeWithData(_enemyData);
        enemyLevelObject.OnObjectDestroyed += OnEnemyDestroyed;
    }

    private void OnEnemyDestroyed(LevelDestroyableObject enemyObject)
    {
        enemyObject.OnObjectDestroyed -= OnEnemyDestroyed;
        var moveController = enemyObject.GetComponent<EnemyMoveController>();
        _spawnedEnemies.Remove(moveController);
        _remainingCount--;
        _enemiesAlive--;
    }

    private IEnumerator SpawnWave(int waveIndex)
    {
        Wave wave = allWaves[waveIndex];

        int spawnedCount = 0;
        
        for (int i = 0; i < wave.enemies.Count; i++)
        {
            if (spawnedCount >= _remainingCount)
                break;
            
            for (int j = 0; j < wave.countOfEnemy[i]; j++)
            {
                SpawnEnemy(wave.enemies[i]);
                spawnedCount++;
                if (spawnedCount >= _remainingCount)
                    break;
                
                yield return new WaitForSeconds(wave.timeBetweenEnemies);
            }
        }
        _waveIndex++;
    }
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (allWaves == null)
            return;
        
        foreach (var wave in allWaves)
        {
            if (wave.enemies.Count > wave.countOfEnemy.Count)
                wave.countOfEnemy.Add(wave.enemies.Count - wave.countOfEnemy.Count);
            else if (wave.enemies.Count < wave.countOfEnemy.Count)
                wave.countOfEnemy.Remove(wave.countOfEnemy.Count - wave.enemies.Count);
            
            wave.totalEnemiesInWave = wave.countOfEnemy.Sum();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.8f, 0f, 0f, 0.4f);
        Gizmos.DrawCube(EnemyMoveZoneCenter, EnemyMoveZoneSize);
    }
#endif
}