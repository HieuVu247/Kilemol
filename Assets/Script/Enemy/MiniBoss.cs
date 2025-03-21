using UnityEngine;
using System.Collections;

public class MiniBoss : EnemyController
{
    public float chargeSpeed = 5f;
    public float chargeDuration = 1f;
    private Transform player;
    private float chargeCooldown = 5f;
    private float lastChargeTime;

    protected override void Start()
    {
        base.Start();
        maxHP = 200f;
        currentHP = maxHP;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange)
        {
            SetState(new AttackState());
        }
        else if (Time.time - lastChargeTime >= chargeCooldown)
        {
            StartCoroutine(Charge());
            lastChargeTime = Time.time;
        }
        else
        {
            MoveState moveState = new MoveState();
            moveState.SetTarget(player.position);
            SetState(moveState);
        }
        currentState.Update(this);
    }

    private IEnumerator Charge()
    {
        moveSpeed = chargeSpeed;
        yield return new WaitForSeconds(chargeDuration);
        moveSpeed = 2f;
    }
}