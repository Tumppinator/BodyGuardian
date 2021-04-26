using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    int waveIndex = 0;

    [SerializeField] Transform[] spawnPoints;


    bool spawn = false;
    bool waveStarted = false;
    int enemyAmount = 0;
    int destroyedEnemies = 0;
    int enemySetIndex = 0;
    int maxEnemies = 0;

    AudioSource audioSource;
    Score score;

    public UnityEvent displayText;


    private void Start()
    {
        score = FindObjectOfType<Score>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(score.GetStartGame() && !waveStarted && waveIndex < waveConfigs.Count)
        {
            spawn = true;
            waveStarted = true;
            displayText.Invoke();
            var currentWave = waveConfigs[waveIndex];
            StartCoroutine(Spawner(currentWave));
        }
    }


    IEnumerator WaitBetweenWaves(WaveConfig currentWave)
    {
        yield return new WaitUntil(() => (destroyedEnemies >= currentWave.GetEnemiesAmountInWave()));
        audioSource.Play();
        yield return new WaitForSeconds(2f);
        waveStarted = false;
        enemySetIndex = 0;
        destroyedEnemies = 0;
        waveIndex++;
    }

    IEnumerator Spawner(WaveConfig currentWave)
    {
        while (spawn)
        {
            yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawnsAtIndex(enemySetIndex));

            maxEnemies = currentWave.GetNumberOfEnemiesAtIndex(enemySetIndex);

            SpawnEnemy(currentWave);
            if (enemyAmount >= maxEnemies)
            {
                enemySetIndex++;
                enemyAmount = 0;
                if(enemySetIndex == currentWave.GetEnemyPrefabsLength())
                {
                    spawn = false;
                    StartCoroutine(WaitBetweenWaves(currentWave));
                }
            }
        }
    }

    private void SpawnEnemy(WaveConfig currentWave)
    {
        Instantiate(currentWave.GetEnemyPrefabsAtIndex(enemySetIndex), GetRandomSpawnPoint().position, GetRandomSpawnPoint().rotation);
        enemyAmount++;
    }

    private Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }



    public void IncreaseDestroyedEnemiesAmount()
    {
        destroyedEnemies++;
    }

    public int GetWaveIndex()
    {
        return waveIndex + 1;
    }

}
