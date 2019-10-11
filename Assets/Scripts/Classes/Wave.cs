using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public float EndTime = 0f;
    public float minSpawnTime = 0f;
    public float maxSpawnTime = 0f;
    public int minEnemies = 0;
    public int maxEnemies = 0;
    public List<EnemyBase> enemies = new List<EnemyBase>();
}
