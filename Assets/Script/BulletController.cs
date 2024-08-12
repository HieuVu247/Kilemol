using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 5f;
    public float sizeMultiplier = 1f; // Hệ số nhân cho kích thước của đạn
    public int damage;

    private void Start()
    {
        // Thiết lập kích thước của đạn theo hệ số nhân
        transform.localScale *= sizeMultiplier;
        // Hủy đạn sau khi hết thời gian sống
        Destroy(gameObject, lifetime);
        //damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().damage;
        //damage = GetComponent<PlayerStats>().damage;
        PlayerStats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        if (playerStats != null)
        {
            damage = (int)playerStats.playerDMG;
            Debug.Log("Bullet damage set to: " + damage);
        }
        else
        {
            Debug.LogError("PlayerStats component not found on player!");
            damage = 10; // Giá trị mặc định nếu không tìm thấy PlayerStats
        }
    }

    private void Update()
    {
        // Di chuyển đạn theo hướng của nó
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Gọi hàm Nhận sát thương của Enemy
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Enemy took damage: " + damage);
            }
            else
            {
                Debug.LogError("EnemyController component not found on enemy!");
            }

            // Hủy đạn
            Destroy(gameObject);
        }
    }
}
