using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int damage = 10;
    public int maxHP = 20;
    private int currentHP;
    private Transform player;
    public int expAmount = 100;


    public float attackRange = 2.0f; // Phạm vi tấn công
    public float detectionRange = 10.0f; // Phạm vi phát hiện
    public float attackCooldown = 2.0f; // Thời gian hồi chiêu giữa các lần tấn công
    private NavMeshAgent agent; // NavMeshAgent cho di chuyển
    private Animator animator; // Animator cho điều khiển hoạt ảnh
    private float lastAttackTime = 0f;



    private void Start()
    {
        currentHP = maxHP;
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        if (player != null)
        {
            // Di chuyển tới vị trí của người chơi
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }


        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            // Đuổi theo player
            agent.SetDestination(player.position);
            animator.SetFloat("Speed", agent.velocity.magnitude);

            if (distanceToPlayer <= attackRange)
            {
                // Tấn công player nếu trong phạm vi
                AttackPlayer();
            }
        }
        else
        {
            // Dừng di chuyển nếu player không ở trong phạm vi phát hiện
            agent.SetDestination(transform.position);
            animator.SetFloat("Speed", 0);
        }
    }

    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            animator.SetTrigger("Attack");
            // Logic tấn công thực tế ở đây, như giảm máu player
            Debug.Log("Enemy attacks player!");
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Destroy(gameObject);
            //LevelSystem.instance.AddEXP(expAmount);
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
