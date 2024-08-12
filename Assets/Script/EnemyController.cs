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

    public void TakeDamage(int playerDMG)
    {
        currentHP -= playerDMG;
        ShowDamePopUp(playerDMG);
        if (currentHP <= 0)
        {
            Destroy(gameObject);
            ScoreManager.instance.AddScore(10);// Thêm 10 điểm mỗi lần bắn trúng Enemy
            LevelSystem.instance.GainEXPFlatRate(Random.Range(50f, 100f));
        }
    }
    private void ShowDamePopUp(int dmgAmount) 
    {
        GameObject popUp = Instantiate(prefabPopUpEnemy,transform.position, Quaternion.identity);
        popUp.GetComponentInChildren<TMP_Text>().text = dmgAmount.ToString();
    }
    public void UpdateDamage(float newDamage)
    {
        damage = (int)newDamage; // Cập nhật sát thương dựa trên `playerDMG` mới
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
