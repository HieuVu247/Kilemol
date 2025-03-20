using UnityEngine;

[CreateAssetMenu(fileName = "PlayerLevelConfig", menuName = "Configs/PlayerLevelConfig")]
public class PlayerLevelConfig : ScriptableObject
{
    public float baseDamage = 10f;
    public float baseHP = 100f;
    public float damageIncreasePerLevel = 2f;
    public float hpIncreasePerLevel = 10f;
}

public class PlayerStats : MonoBehaviour
{
    public PlayerLevelConfig config;
    private int level = 1;
    private float currentHP;
    public float CurrentDamage { get; private set; }
    public float CurrentHP => currentHP;

    private void Start()
    {
        CurrentDamage = config.baseDamage;
        currentHP = config.baseHP;
    }

    public void LevelUp()
    {
        level++;
        CurrentDamage += config.damageIncreasePerLevel;
        currentHP = config.baseHP + config.hpIncreasePerLevel * (level - 1);
        Debug.Log($"Level Up! Level: {level}, Damage: {CurrentDamage}, HP: {currentHP}");
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        DamagePopup.Create(transform.position, damage);
        if (currentHP <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}