    using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int damage = 10;
    public int maxHP = 20;
    private int currentHP;
    private Transform player;
    public GameObject prefabPopUpEnemy;
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
        ShowDamePopUp(damage);
        if (currentHP <= 0)
        {
            Destroy(gameObject);
            ScoreManager.instance.AddScore(10);// Thêm 10 điểm mỗi lần bắn trúng Enemy
        }
    }
    private void ShowDamePopUp(int dameAmount) 
    {
        GameObject popUp = Instantiate(prefabPopUpEnemy,transform.position, Quaternion.identity);
        popUp.GetComponentInChildren<TMP_Text>().text = dameAmount.ToString();
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
