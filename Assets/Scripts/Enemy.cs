using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform Player; // Tham chiếu đến vị trí của Player
    public float moveSpeed = 3f; // Tốc độ di chuyển của Enemy

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Player != null)
        {
            Vector2 direction = (Player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (Player != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Player.position);
        }
    }

    public void OnHit()
    {
        // Xử lý khi Enemy bị bắn trúng (ví dụ: hiệu ứng nổ, âm thanh, vv.)
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats playerHealth = collision.GetComponent<PlayerStats>();
            if (!playerHealth.isInvincible) // Kiểm tra nếu Player không miễn nhiễm sát thương
            {
                playerHealth.TakeDamage(10); // Gọi hàm TakeDamage của PlayerHealth
            }
        }
    }
}
