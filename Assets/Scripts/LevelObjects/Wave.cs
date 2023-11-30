using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public List<GameObject> enemies;
    public List<int> countOfEnemy;
    public float timeBetweenEnemies;
    public int totalEnemiesInWave;
}