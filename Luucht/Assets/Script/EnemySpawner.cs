using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Wave
{
    public string waveName;
    public int noOfEnemies;
    public GameObject pooledEnemyPrefab; 
    public int poolSize; 
    public float spawnInterval;
}

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;
    public Transform[] spawnPoints;

    private Wave currentWave;
    private int currentWaveNumber;
    private float nextSpawnTime;
    private bool canSpawn = true;

    private List<GameObject> pooledEnemies = new List<GameObject>();

    private void Start()
    {
        InitializePool();
    }

    private void Update()
    {
        currentWave = waves[currentWaveNumber];
        SpawnWave();
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0 && !canSpawn && currentWaveNumber + 1 != waves.Length)
        {
            currentWaveNumber++;
            canSpawn = true;
        }
    }

    private void InitializePool()
    {
        foreach (Wave wave in waves)
        {
            GameObject enemyContainer = new GameObject(wave.waveName + " Enemies");
            enemyContainer.transform.parent = transform;

            for (int i = 0; i < wave.poolSize; i++)
            {
                GameObject enemy = Instantiate(wave.pooledEnemyPrefab, Vector3.zero, Quaternion.identity);
                enemy.SetActive(false);
                enemy.transform.parent = enemyContainer.transform;
                pooledEnemies.Add(enemy);
            }
        }
    }

    private void SpawnWave()
    {
        if (canSpawn && nextSpawnTime < Time.time)
        {
            GameObject enemyToSpawn = GetPooledEnemy(currentWave.pooledEnemyPrefab);
            if (enemyToSpawn != null)
            {
                Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                enemyToSpawn.transform.position = randomPoint.position;
                enemyToSpawn.transform.rotation = Quaternion.identity;
                enemyToSpawn.SetActive(true);

                currentWave.noOfEnemies--;
                nextSpawnTime = Time.time + currentWave.spawnInterval;

                if (currentWave.noOfEnemies == 0)
                {
                    canSpawn = false;
                }
            }
        }
    }

    private GameObject GetPooledEnemy(GameObject enemyPrefab)
    {
        for (int i = 0; i < pooledEnemies.Count; i++)
        {
            if (!pooledEnemies[i].activeInHierarchy && pooledEnemies[i].gameObject.CompareTag(enemyPrefab.tag))
            {
                return pooledEnemies[i];
            }
        }
        return null;
    }
}
