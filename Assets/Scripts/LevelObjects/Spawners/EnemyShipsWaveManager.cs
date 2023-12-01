using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyShipsWaveManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField, Range(1, 10)] private float enemyMoveZoneWidth, enemyMoveZoneHeight;
    [SerializeField] private Vector3 enemyMoveZoneOffset;

    [SerializeField] private float startDelay;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private bool waitForLastEnemy;

    [SerializeField] private float countdownToNewWave = 0;
    [SerializeField] private int waveIndex = 0;
    [SerializeField] private int enemiesAlive;

    [SerializeField] private Wave[] allWaves;
    
    private Vector3 EnemyMoveZoneCenter => transform.position + enemyMoveZoneOffset;
    private Vector3 EnemyMoveZoneSize => new(enemyMoveZoneWidth, enemyMoveZoneHeight, 0f);

    private List<GameObject> _spawnedEnemies;
    
    public Vector3 MovingZoneSize(Vector3 center, float sizeX, float sizeY)
    {
        Vector3 zero = Vector3.zero;
        return zero;
    }

    public void EnemyDestroyed(GameObject enemy) => _spawnedEnemies.Remove(enemy);

    private void Awake()
    {
        _spawnedEnemies = new List<GameObject>();
        countdownToNewWave = startDelay;
    }

    private void Update()
    {
        if (waitForLastEnemy && enemiesAlive > 0)
            return;

        if (countdownToNewWave <= 0 && waveIndex < allWaves.Length)
        {
            if (allWaves[waveIndex].totalEnemiesInWave > 0)
            {
                StartCoroutine(SpawnWave(waveIndex));
                countdownToNewWave = timeBetweenWaves;
            }
            else return;
        }

        countdownToNewWave -= Time.deltaTime;
        countdownToNewWave = Mathf.Clamp(countdownToNewWave, 0, timeBetweenWaves);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
        foreach (var spawnedEnemy in _spawnedEnemies)
        {
            Destroy(spawnedEnemy.gameObject);
        }
        _spawnedEnemies.Clear();
    }

    private void SpawnEnemy(GameObject enemy)
    {
        int i = Mathf.RoundToInt(Random.Range(0, spawnPoints.Length));
        enemiesAlive++;
        GameObject newEnemy = Instantiate(enemy, spawnPoints[i].position, enemy.transform.rotation);
        newEnemy.GetComponent<EnemyMoveController>().Init(EnemyMoveZoneCenter, EnemyMoveZoneSize, this);
        _spawnedEnemies.Add(newEnemy);
    }

    private IEnumerator SpawnWave(int waveIndex)
    {
        Wave wave = allWaves[waveIndex];
        //enemiesAlive += wave.countOfEnemy.Sum();

        for (int i = 0; i < wave.enemies.Count; i++)
        {
            for (int j = 0; j < wave.countOfEnemy[i]; j++)
            {
                SpawnEnemy(wave.enemies[i]);
                yield return new WaitForSeconds(wave.timeBetweenEnemies);
            }
        }
        this.waveIndex++;
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