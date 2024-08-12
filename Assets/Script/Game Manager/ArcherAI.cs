using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAI : MonoBehaviour
{
    public Transform player;  // Tham chiếu đến Transform của người chơi
    public float speed = 2f;  // Tốc độ di chuyển của kẻ địch
    private SpriteRenderer spriteRenderer; // Tham chiếu tới SpriteRenderer để lật sprite

    // Shoot
    public GameObject bullet;        // Prefab của đạn
    public float bulletSpeed = 5f;   // Tốc độ của đạn
    public float timeBtwFire = 2f;   // Thời gian giữa các lần bắn
    private float fireCooldown;      // Biến đếm ngược thời gian giữa các lần bắn

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fireCooldown = timeBtwFire;  // Đặt thời gian đếm ngược ban đầu
    }

    void Update()
    {
        if (player == null)
            return;

        // Tính toán hướng từ kẻ địch đến người chơi
        Vector3 direction = player.position - transform.position;
        direction.Normalize();  // Chuẩn hóa vector hướng

        // Di chuyển kẻ địch về phía người chơi
        transform.position += direction * speed * Time.deltaTime;

        // Xoay mặt kẻ địch về phía người chơi
        spriteRenderer.flipX = direction.x < 0;

        // Kiểm tra nếu đủ thời gian để bắn
        fireCooldown -= Time.deltaTime;
        if (fireCooldown <= 0f)
        {
            fireCooldown = timeBtwFire;  // Đặt lại thời gian giữa các lần bắn
            EnemyFireBullet();           // Thực hiện việc bắn đạn
        }
    }

    void EnemyFireBullet()
    {
        // Tạo một viên đạn mới tại vị trí của enemy
        GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);

        // Lấy thành phần Rigidbody2D của viên đạn
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();

        // Tính toán hướng bắn dựa trên vị trí người chơi
        Vector2 shootDirection = (player.position - transform.position).normalized;

        // Thêm lực để bắn đạn về hướng người chơi
        rb.AddForce(shootDirection * bulletSpeed, ForceMode2D.Impulse);
    }


}
