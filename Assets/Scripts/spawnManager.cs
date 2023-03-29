using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesPrefabs;
    private GameObject[] enemiesPool;
    private int currentEnemy = 0, enemiesPoolSize = 8;

    public static int enemyCount;
    public static int waveNumber = 0;

    [SerializeField] private TextMeshProUGUI waveNumberText;
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
        if (enemyCount == 0 && !GameManager.Instance.isGameOver)
        {
            waveNumber++;
            GameManager.Instance.isFighting = false;
            SpawnEnemyWave(waveNumber);
        }
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        if (!GameManager.Instance.isFighting)
        {
            float enemyATKEncrease = 0.1f * waveNumber;
            float enemyHPEncrease = 2 * waveNumber;
            EnemyController.attack += enemyATKEncrease;
            EnemyController.hp += enemyHPEncrease;

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                if (currentEnemy >= enemiesPoolSize)
                    currentEnemy = 0;
                enemiesPool[currentEnemy].SetActive(true);
                enemiesPool[currentEnemy].transform.position = GenerateRandomPos();

                Healthbar.SetNewHP(enemiesPool[currentEnemy]);
                currentEnemy++;
            }
            waveNumberText.text = "Wave: " + waveNumber.ToString();
        }
    }

    private Vector3 GenerateRandomPos()
    {
        float randomZ = Random.Range(-10, -1);
        float randomX = Random.Range(19, 29);
        return new Vector3(randomX, 0.9f, randomZ);
    }
}
