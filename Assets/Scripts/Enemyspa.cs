using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspa : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab của Enemy
    public Transform Player; // Tham chiếu đến vị trí của Player
    public float spawnInterval = 2f; // Thời gian giữa các lần xuất hiện
    public float spawnRadius = 10f; // Bán kính xuất hiện xung quanh trung tâm

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Enemy enemyMovement = newEnemy.GetComponent<Enemy>();
        if (enemyMovement != null)
        {
            enemyMovement.Player = Player;
        }
    }
}
