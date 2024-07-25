using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspa : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab của enemy
    public Transform player; // Vị trí của người chơi
    public float spawnDistance = 10f; // Khoảng cách tối thiểu từ camera để spawn enemy
    public float spawnInterval = 2f; // Thời gian giữa các lần spawn

    private float timer = 0f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = mainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * screenAspect;

        // Chọn ngẫu nhiên một cạnh ngoài camera để sinh enemy
        int side = Random.Range(0, 4);

        switch (side)
        {
            case 0: // Trên cùng
                spawnPosition = new Vector3(Random.Range(-cameraWidth / 2, cameraWidth / 2),
                                            mainCamera.transform.position.y + mainCamera.orthographicSize + spawnDistance,
                                            0);
                break;
            case 1: // Dưới cùng
                spawnPosition = new Vector3(Random.Range(-cameraWidth / 2, cameraWidth / 2),
                                            mainCamera.transform.position.y - mainCamera.orthographicSize - spawnDistance,
                                            0);
                break;
            case 2: // Bên trái
                spawnPosition = new Vector3(mainCamera.transform.position.x - cameraWidth / 2 - spawnDistance,
                                            Random.Range(-cameraHeight / 2, cameraHeight / 2),
                                            0);
                break;
            case 3: // Bên phải
                spawnPosition = new Vector3(mainCamera.transform.position.x + cameraWidth / 2 + spawnDistance,
                                            Random.Range(-cameraHeight / 2, cameraHeight / 2),
                                            0);
                break;
        }

        return spawnPosition;
    }
}
