using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHP = 100; //HP của nhân vật
    [SerializeField] private int currentHP;

    public int damage = 10; // Sát thương của nhân vật

    public float invincibilityDuration = 1f; // Thời gian miễn nhiễm sát thương sau khi nhận sát thương
    public bool isInvincible = false;
    private float invincibilityTimer;

    private List<Collider2D> collidingEnemies = new List<Collider2D>();
    private void Start()
    {
        currentHP = maxHP;
    }
    //Chức năng Nhận sát thương của Player
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHP -= damage;
            if (currentHP <= 0)
            {
                // Xử lý khi nhân vật hết máu, ví dụ: kết thúc trò chơi
                Debug.Log("Player is dead!");
            }
            isInvincible = true;
            StartCoroutine(InvincibilityCooldown());
        }
    }
    //Hàm chạy sau khi hết thời gian miễn nhiễm sát thương của nhân vật
    private IEnumerator InvincibilityCooldown()
    {
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
        if (collidingEnemies.Count > 0)//Sau đó kiểm tra xem có còn Quái tiếp tục gây sát thương cho Nhân vật hay không
        {
            StartCoroutine(ApplyContinuousDamage());//Gọi hàm gây sát thương liên tục
        }
    }
    //Chức năng phtas hiện va chạm với Quái
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !collidingEnemies.Contains(collision))
        {
            collidingEnemies.Add(collision);
            if (!isInvincible)
            {
                TakeDamage(10);
                StartCoroutine(InvincibilityCooldown());//Kiểm tra và Reset trạng thái miễn nhiễm sát thương của Nhân vật
            }
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collidingEnemies.Remove(collision);
        }
    }
    //Chức năng Nhận sát thương liên tục khi chồng chéo Quái
    private IEnumerator ApplyContinuousDamage()
    {
        while (collidingEnemies.Count > 0 && !isInvincible)
        {
            TakeDamage(10);
            yield return new WaitForSeconds(invincibilityDuration);
        }
    }
}
