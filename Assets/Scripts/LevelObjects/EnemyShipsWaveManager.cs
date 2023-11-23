using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyShipsWaveManager : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField, Range(1, 10)] float enemyMoveZoneWidth, enemyMoveZoneHeight;
    [SerializeField] Vector3 enemyMoveZoneOffset;

    [SerializeField] float startDelay;
    [SerializeField] float timeBetweenWaves;
    [SerializeField] bool waitForLastEnemy;

    [SerializeField] float countdownToNewWave = 0;
    [SerializeField] int waveIndex = 0;
    [SerializeField] int enemiesAlive;
    public static EnemyShipsWaveManager instance;

    [SerializeField] Wave[] allWaves;

    public Vector3 EnemyMoveZoneCenter => transform.position + enemyMoveZoneOffset;
    public Vector3 EnemyMoveZoneSize => new Vector3(enemyMoveZoneWidth, enemyMoveZoneHeight, 0f);

    public Vector3 MovingZoneSize(Vector3 center, float sizeX, float sizeY)
    {
        Vector3 zero = Vector3.zero;
        return zero;
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(instance);

        countdownToNewWave = startDelay;
    }

    private void Update()
    {
        if (waitForLastEnemy && enemiesAlive > 0) return;

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

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (allWaves.Length < 0) return;
        for (int i = 0; i < allWaves.Length; i++)
        {
            if (allWaves[i].enemies.Count > allWaves[i].countOfEnemy.Count)
            {
                allWaves[i].countOfEnemy.Add(allWaves[i].enemies.Count - allWaves[i].countOfEnemy.Count);
            }
            else if (allWaves[i].enemies.Count < allWaves[i].countOfEnemy.Count)
            {
                allWaves[i].countOfEnemy.Remove(allWaves[i].countOfEnemy.Count - allWaves[i].enemies.Count);
            }
            allWaves[i].totalEnemiesInWave = allWaves[i].countOfEnemy.Sum();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.8f, 0f, 0f, 0.4f);
        Gizmos.DrawCube(EnemyMoveZoneCenter, EnemyMoveZoneSize);
    }
#endif

    private void SpawnEnemy(GameObject enemy)
    {
        int i = Mathf.RoundToInt(Random.Range(0, spawnPoints.Length));
        enemiesAlive++;
        GameObject newEnemy = Instantiate(enemy, spawnPoints[i].position, enemy.transform.rotation);
        newEnemy.GetComponent<EnemyMoveController>().Init(EnemyMoveZoneCenter, EnemyMoveZoneSize, this);
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

    private void StopSpawn() => StopAllCoroutines();
    public void ReduceAliveEnemies() => enemiesAlive--;

}

[System.Serializable]
public class Wave
{
    public List<GameObject> enemies;
    public List<int> countOfEnemy;
    public float timeBetweenEnemies;
    public int totalEnemiesInWave;
}
