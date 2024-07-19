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
        damage = 10;
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
            //Gọi hàm Nhận sát thương của Enemy
            collision.GetComponent<EnemyController>().TakeDamage(damage);

            // Hủy đạn
            Destroy(gameObject);
        }
        //else if (collision.CompareTag("Wall"))
        //{
        //    // Hủy đạn khi va chạm với tường
        //    Destroy(gameObject);
        //}
    }
}
