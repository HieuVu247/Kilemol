using UnityEngine;
using System.Collections;

public class EnemyFactory
{
    private GameObject[] enemyPrefabs;

    public EnemyFactory(GameObject[] prefabs)
    {
        enemyPrefabs = prefabs;
    }

    public GameObject CreateEnemy(int type, Vector3 position)
    {
        return Object.Instantiate(enemyPrefabs[type], position, Quaternion.identity); // Sửa từ Instatiate thành Instantiate
    }
}

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // 0: Basic, 1: Ranged, 2: MiniBoss
    public Transform player;
    public float spawnRadius = 10f;
    public float spawnInterval = 2f;
    private float gameTime;
    private EnemyFactory enemyFactory;

    private void Start()
    {
        enemyFactory = new EnemyFactory(enemyPrefabs);
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        gameTime += Time.deltaTime;
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector2 spawnPos = player.position + (Vector3)Random.insideUnitCircle.normalized * spawnRadius;
            if (gameTime < 120f) // 2 phút
                enemyFactory.CreateEnemy(0, spawnPos);
            else if (gameTime < 300f) // 5 phút
                enemyFactory.CreateEnemy(Random.Range(0, 2), spawnPos);
            else if (gameTime < 480f) // 8 phút
                enemyFactory.CreateEnemy(2, spawnPos);
            else
            {
                break; // Final Boss logic
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}