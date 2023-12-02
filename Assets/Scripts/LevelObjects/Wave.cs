using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    public List<EnemyMoveController> enemies;
    public List<int> countOfEnemy;
    public float timeBetweenEnemies;
    public int totalEnemiesInWave;
}