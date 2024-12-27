using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnetSpawner : MonoBehaviour
{
    public GameObject coinMagnetPrefab; // Prefab cho item hút xu
    public float spawnRate = 10f; // Tần suất xuất hiện
    public float spawnRadius = 10f; // Bán kính xuất hiện

    private void Start()
    {
        // Khởi động việc xuất hiện ngẫu nhiên
        InvokeRepeating("SpawnCoinMagnet", 0f, spawnRate);
    }

    private void SpawnCoinMagnet()
    {
        // Tạo item hút xu tại vị trí ngẫu nhiên trong bán kính
        Vector3 randomPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        Instantiate(coinMagnetPrefab, randomPosition, Quaternion.identity);
    }
}
