using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Spawner : MonoBehaviour
{
    [SerializeField] Enemy1 enemyPrefab;
    [SerializeField] float minSpawnDelay = 1f;
    [SerializeField] float maxSpawnDelay = 6f;

    [SerializeField] int maxEnemies = 3;
    bool spawn = true;
    int enemyAmount = 0;

    IEnumerator Start()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnEnemy();
            if(enemyAmount > maxEnemies)
            {
                spawn = false;
            }
        }
    }


    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, transform.rotation);
        enemyAmount++;
    }

}
