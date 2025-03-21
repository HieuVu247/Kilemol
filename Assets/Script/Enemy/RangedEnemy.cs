using UnityEngine;

public class RangedEnemy : EnemyController
{
    public float shootRange = 5f;
    public GameObject projectilePrefab;
    private Transform player;
    private float attackCooldown = 2f;
    private float lastAttackTime;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= shootRange && Time.time - lastAttackTime >= attackCooldown)
        {
            SetState(new AttackState());
            lastAttackTime = Time.time;
            Shoot();
        }
        else if (distanceToPlayer > shootRange)
        {
            MoveState moveState = new MoveState();
            moveState.SetTarget(player.position);
            SetState(moveState);
        }
        currentState.Update(this);
    }

    private void Shoot()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * 5f;
    }
}