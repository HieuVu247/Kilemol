using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyban : MonoBehaviour
{
    public GameObject bulletPrefab;  // Prefab của đạn
    public Transform bulletSpawnPoint;  // Vị trí mà đạn sẽ được bắn ra
    public float shootingInterval = 2f;  // Thời gian giữa mỗi lần bắn đạn
    public Transform player;  // Tham chiếu đến Transform của người chơi

    void Start()
    {
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootingInterval);
            FireBullet();
        }
    }

    void FireBullet()
    {
        if (player == null)
            return;

        // Tính toán hướng từ kẻ địch đến người chơi
        Vector2 direction = (player.position - bulletSpawnPoint.position).normalized;

        // Tạo đạn tại vị trí bulletSpawnPoint
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);

        // Thiết lập hướng của đạn
        bullet.transform.right = direction;

        // Đẩy đạn về phía người chơi
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bullet.GetComponent<bulletcung>().speed;
        }
    }
}
