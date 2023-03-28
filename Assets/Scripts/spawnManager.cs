using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesPrefabs;
    private GameObject[] enemiesPool;
    private int currentEnemy = 0, enemiesPoolSize = 5;
    private float spawnPosX = 25;

    private int enemyCount, waveNumber;
    void Start()
    {
        enemiesPool = new GameObject[enemiesPoolSize];
        for (int i = 0; i < enemiesPoolSize; i++)
        {
            int randomEnemy = Random.Range(0, enemiesPrefabs.Length);
            enemiesPool[i] = Instantiate(enemiesPrefabs[randomEnemy]);
            enemiesPool[i].gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if (enemyCount == 0)
        {
            GameManager.Instance.isFighting = false;
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        if (!GameManager.Instance.isGameOver && !GameManager.Instance.isFighting)
        {
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                if (currentEnemy >= enemiesPoolSize)
                    currentEnemy = 0;
                enemiesPool[currentEnemy].SetActive(true);
                enemiesPool[currentEnemy].transform.position = GenerateRandomPos();
                currentEnemy++;

                EnemyController.hp += 10 *  waveNumber;
                EnemyController.attack +=  2 * waveNumber;
            }
        }
    }

    private Vector3 GenerateRandomPos()
    {
        float randomZ = Random.Range(-10, -1);
        return new Vector3(spawnPosX, 1.35f, randomZ);
    }
}
