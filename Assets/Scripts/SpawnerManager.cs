using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public float startTimeBtwSpawn;
    private float timeBtwSpawn;

    [Header("Fix enemy")]
    public Transform parentEnemyy;
    public int enemyCount = 15;
    public List<Transform> enemiesOnScene;

    public GameObject[] enemies;

    public WeaponManager weaponManager;

    public List<Spawner> spawners;
    public GameObject boss;

    private Player player;
    int maxEnemy = 5;
    int roundCount = 0;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public List<int> GetRandomIndices(int n, int k)
    {

        // Create a list containing all indices from 0 to n-1
        List<int> allIndices = new List<int>();
        for (int i = 0; i < n; i++)
        {
            allIndices.Add(i);
        }

        // Create a list to store the randomly selected indices
        List<int> randomIndices = new List<int>();

        // Use Fisher-Yates shuffle algorithm to randomly shuffle the indices
        int remainingItems = n;
        for (int i = 0; i < k; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, remainingItems);
            randomIndices.Add(allIndices[randomIndex]);
            // Move the last index in the list to the current position
            allIndices[randomIndex] = allIndices[remainingItems - 1];
            remainingItems--;
        }

        return randomIndices;
    }

    private void Update()
    {
        GetListEnemy();
        if (enemiesOnScene.Count < enemyCount)
        {
            Spawn();
        }
        else
        {
            return;
        }
    }

    void GetListEnemy()
    {
        enemiesOnScene = new List<Transform>();
        foreach (Transform child in parentEnemyy)
        {
            if (!enemiesOnScene.Contains(child))
            {
                enemiesOnScene.Add(child);
            }
        }
    }

    void Spawn()
    {
        if (timeBtwSpawn <= 0)
        {
            int randEnemyCount = UnityEngine.Random.Range(2, maxEnemy); // Xác định số lượng kẻ địch ngẫu nhiên sẽ spawn 
            if (weaponManager.Enemies.Count <= 5)           // Nếu số lượng kẻ địch hiện tại <= 5, tăng số lượng spawn -- tang do kho   
                randEnemyCount = UnityEngine.Random.Range(maxEnemy - 2, maxEnemy);

            List<int> randomIndex = GetRandomIndices(maxEnemy, randEnemyCount);

            foreach (int index in randomIndex)      // Với mỗi vị trí spawn ngẫu nhiên, spawn kẻ địch
            {
                int randEnemy = UnityEngine.Random.Range(0, enemies.Length);
                spawners[index].spawnEnemy(enemies[randEnemy]);
            }
            timeBtwSpawn = startTimeBtwSpawn;

            roundCount++;
            if (roundCount > 10)        // Sau mỗi 10 vòng spawn, xuất hiện boss
            {
                int bossSpawnerIndex = UnityEngine.Random.Range(0, spawners.Count);
                spawners[bossSpawnerIndex].spawnEnemy(boss);
                roundCount = 0;
                maxEnemy = Mathf.Max(enemyCount, maxEnemy + 1);     // Tăng số lượng kẻ địch tối đa để tăng độ khó
            }
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
