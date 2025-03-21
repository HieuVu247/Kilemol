using UnityEngine;

public interface IEnemyState
{
    void Enter(EnemyController enemy);
    void Update(EnemyController enemy);
    void Exit(EnemyController enemy);
}

public abstract class EnemyController : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rb;
    public float moveSpeed = 2f;
    public float attackRange = 1f;
    public float damage = 5f;
    private float attackCooldown = 1f;
    private float lastAttackTime;
    public float maxHP = 50f;
    public float expValue = 10f; // EXP mà Player nhận khi quái chết
    protected float currentHP;
    protected IEnemyState currentState;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        currentState = new IdleState();
        currentState.Enter(this);
    }

    private void Update()
    {
        currentState.Update(this);
    }

    public void SetState(IEnemyState newState)
    {
        currentState.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }

    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        DamagePopup.Create(transform.position, damage);
        if (currentHP <= 0) Die();
    }

    protected virtual void Die()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.AddEXP(expValue); // Cộng EXP cho Player
        }
        Destroy(gameObject);
    }

    // Phương thức công khai để điều khiển Animator
    public void SetMovingAnimation(bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
    }

    public void TriggerAttackAnimation()
    {
        animator.SetTrigger("attack");
    }
    // Phương thức công khai để điều khiển Rigidbody2D
    public void SetVelocity(Vector2 velocity)
    {
        rb.linearVelocity = velocity;
    }
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time - lastAttackTime >= attackCooldown)
        {
            PlayerStats playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                playerStats.TakeDamage(damage);
                TriggerAttackAnimation();
                lastAttackTime = Time.time;
            }
        }
    }
}

public class IdleState : IEnemyState
{
    public void Enter(EnemyController enemy) { enemy.SetMovingAnimation(false); }
    public void Update(EnemyController enemy) { }
    public void Exit(EnemyController enemy) { }
}

public class MoveState : IEnemyState
{
    private Vector3 target;
    public void Enter(EnemyController enemy) { enemy.SetMovingAnimation(true); }
    public void Update(EnemyController enemy)
    {
        Vector2 direction = (target - enemy.transform.position).normalized;
        enemy.SetVelocity(direction * enemy.moveSpeed); // Sửa dòng 80
    }
        public void Exit(EnemyController enemy) { enemy.SetVelocity(Vector2.zero); } // Sửa dòng 82
        public void SetTarget(Vector3 newTarget) { target = newTarget; }
}

public class AttackState : IEnemyState
{
    public void Enter(EnemyController enemy) { enemy.TriggerAttackAnimation(); }
    public void Update(EnemyController enemy) { }
    public void Exit(EnemyController enemy) { }
}