using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] float[] timeBetweenSpawns;
    [SerializeField] int[] numberOfEnemies;

    public GameObject GetEnemyPrefabsAtIndex(int index) { return enemyPrefabs[index]; }


    public float GetTimeBetweenSpawnsAtIndex(int index) { return timeBetweenSpawns[index]; }

    public int GetNumberOfEnemiesAtIndex(int index) { return numberOfEnemies[index]; }


    public int GetEnemyPrefabsLength()
    {
        return enemyPrefabs.Length;
    }

    public int GetEnemiesAmountInWave()
    {
        int enemyAmount = 0;
        foreach (int enemies in numberOfEnemies)
        {
            enemyAmount += enemies;
        }
        return enemyAmount;

    }
}
