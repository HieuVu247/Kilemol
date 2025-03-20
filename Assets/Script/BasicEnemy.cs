using UnityEngine;

public class BasicEnemy : EnemyController
{
    private Transform player;
    private MoveState moveState;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        moveState = new MoveState();
        SetState(moveState);
    }

    private void Update()
    {
        moveState.SetTarget(player.position);
        currentState.Update(this);
    }
}