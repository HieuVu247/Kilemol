using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStats : MonoBehaviour
{
    private float maxHP = 100; //HP của nhân vật
    private float currentHP;

    private float lerpTimer;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthText;

    public float damage = 10; // Sát thương của nhân vật

    public float invincibilityDuration = 1f; // Thời gian miễn nhiễm sát thương sau khi nhận sát thương
    public bool isInvincible = false;
    private float invincibilityTimer;
    public GameObject popUpPrefab;

    private List<Collider2D> collidingEnemies = new List<Collider2D>();
    void Start()
    {
        currentHP = maxHP;
    }
    //Chức năng Nhận sát thương của Player
    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            currentHP -= damage;
            ShowDamePopUp(damage);
            if (currentHP <= 0f)
            {
                // Xử lý khi nhân vật hết máu, ví dụ: kết thúc trò chơi
                Debug.Log("Player is dead!");
            }
            isInvincible = true;
            StartCoroutine(InvincibilityCooldown());
        }
        lerpTimer = 0f;
    }
    private void ShowDamePopUp(float dmgAmount)
    {
        GameObject popUp = Instantiate(popUpPrefab, transform.position, Quaternion.identity);
        popUp.GetComponentInChildren<TMP_Text>().text = dmgAmount.ToString();
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
    //Chức năng phát hiện va chạm với Quái
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

    void Update()
    {
        currentHP= Mathf.Clamp(currentHP, 0, maxHP);
        UpdateHealthUI();
        //Bấm test mất máu 
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(Random.Range(5, 10));
        }
        //Bấm test hồi máu
        if (Input.GetKeyDown(KeyCode.H))
        {
            RestoreHealth(Random.Range(5, 10));
        }
    }
    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = currentHP / maxHP;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.yellow;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
        healthText.text = Mathf.Round(currentHP) + "/" + Mathf.Round(maxHP);
    }
    public void RestoreHealth(float healAmount)
    {
        currentHP += healAmount;
        lerpTimer = 0f;
    }
    public void IncreaseHealth(int level)
    {
        maxHP += (maxHP * 0.01f) * ((100 - level) * 0.1f);
        currentHP = maxHP;
    }
    public void IncreaseDamage(int level)
    {
        damage += (damage * 0.5f) * ((100 - level) * 0.1f);
    }
}
