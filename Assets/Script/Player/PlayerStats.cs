using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[CreateAssetMenu(fileName = "PlayerLevelConfig", menuName = "Configs/PlayerLevelConfig")]
public class PlayerLevelConfig : ScriptableObject
{
    public float baseDamage = 10f;
    public float baseHP = 100f;
    public float damageIncreasePerLevel = 2f;
    public float hpIncreasePerLevel = 10f;
    public float expToLevelUp = 50f; // EXP cần để lên cấp
    public float expIncreasePerLevel = 20f; // Tăng EXP cần khi lên cấp
}

public class PlayerStats : MonoBehaviour
{
    public PlayerLevelConfig config;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI expText;
    public TextMeshProUGUI levelText; // Thêm Text cho Level
    public Slider hpSlider; // Slider cho HP
    public Slider expSlider; // Slider cho EXP

    private int level = 1;
    private float currentHP;
    private float targetHP; // HP mục tiêu để Lerp tới
    private float currentEXP;
    private float maxEXP; // EXP tối đa để lên cấp
    public float CurrentDamage { get; private set; }
    public float CurrentHP => currentHP;
    
    public AudioSource audioSource;
    public AudioClip hitSound;

    private void Start()
    {
        CurrentDamage = config.baseDamage;
        currentHP = config.baseHP;
        targetHP = currentHP; // Đồng bộ ban đầu
        currentEXP = 0f;
        maxEXP = config.expToLevelUp;

        // Thiết lập Slider
        hpSlider.maxValue = config.baseHP;
        hpSlider.value = currentHP;
        expSlider.maxValue = maxEXP;
        expSlider.value = currentEXP;
        UpdateUI();

        StartCoroutine(UpdateHPSmoothly()); // Bắt đầu coroutine để cập nhật HP mượt mà
        audioSource = GetComponent<AudioSource>();
    }

    private void UpdateUI()
    {
        hpText.text = $"{Mathf.Round(currentHP)}/{hpSlider.maxValue}";
        expText.text = $"{Mathf.Round(currentEXP)}/{maxEXP}";
        levelText.text = $"{level}"; // Hiển thị level hiện tại
    }

    public void LevelUp()
    {
        level++;
        CurrentDamage += config.damageIncreasePerLevel;
        currentHP = config.baseHP + config.hpIncreasePerLevel * (level - 1);
        targetHP = currentHP; // Cập nhật targetHP khi lên cấp
        maxEXP += config.expIncreasePerLevel; // Tăng EXP cần để lên cấp tiếp theo
        currentEXP = 0f; // Reset EXP sau khi lên cấp

        // Cập nhật Slider
        hpSlider.maxValue = config.baseHP + config.hpIncreasePerLevel * (level - 1);
        hpSlider.value = currentHP;
        expSlider.maxValue = maxEXP;
        expSlider.value = currentEXP;
        UpdateUI();

        Debug.Log($"Level Up! Level: {level}, Damage: {CurrentDamage}, HP: {currentHP}");
    }

    public void TakeDamage(float damage)
    {
        targetHP = Mathf.Max(0, targetHP - damage); // Giảm targetHP
        DamagePopup.Create(transform.position, damage);
        if (targetHP <= 0) Die();
        audioSource.PlayOneShot(hitSound); // Thêm âm thanh
    }

    public void AddEXP(float exp)
    {
        currentEXP += exp;
        expSlider.value = currentEXP; // Cập nhật Slider EXP
        UpdateUI();
        if (currentEXP >= maxEXP)
        {
            LevelUp();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died!");
        Destroy(gameObject);
    }

    // Coroutine để giảm HP mượt mà
    private IEnumerator UpdateHPSmoothly()
    {
        while (true)
        {
            if (currentHP != targetHP)
            {
                currentHP = Mathf.Lerp(currentHP, targetHP, Time.deltaTime * 5f); // Tốc độ giảm HP
                hpSlider.value = currentHP;
                UpdateUI(); // Cập nhật UI trong quá trình giảm
            }
            yield return null; // Chờ frame tiếp theo
        }
    }
}