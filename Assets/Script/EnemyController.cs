using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int damage = 10;
    public int maxHP = 20;
    private int currentHP;
    private Transform player;
    public int expAmount = 100;
    private void Start()
    {
        currentHP = maxHP;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    private void Update()
    {
        if (player != null)
        {
            // Di chuyển tới vị trí của người chơi
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Destroy(gameObject);
            LevelSystem.instance.AddEXP(expAmount);
            ScoreManager.instance.AddScore(10);// Thêm 10 điểm mỗi lần bắn trúng Enemy
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats playerHealth = collision.GetComponent<PlayerStats>();
            if (playerHealth != null && !playerHealth.isInvincible)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

}
