using UnityEngine;

public class BossPatrolState : IBossState
{
    private BurnerBeetleAI boss;
    private Vector3 pointA;
    private Vector3 pointB;
    private bool movingToB = true;
    private float patrolRange = 5f;

    public BossPatrolState(BurnerBeetleAI boss)
    {
        this.boss = boss;
        pointA = boss.StartPosition - new Vector3(patrolRange, 0, 0);
        pointB = boss.StartPosition + new Vector3(patrolRange, 0, 0);
    }

    public void Enter()
    {
       Debug.Log("Entering Patrol State");
    }
    
    public void Update()
    {
        float distanceToPlayer = Vector2.Distance(boss.transform.position, boss.Player.position);
        if (boss.GetStateElapsedTime() > 4f)
        {
            boss.SwitchState(new BossConeFireState(boss));
            Debug.Log("Switching to Cone Fire State");
        }
    }

    public void FixedUpdate()
    {
        Vector3 target = movingToB ? pointB : pointA;
        Vector2 direction = (target - boss.transform.position).normalized;
        boss.Rb.linearVelocity = new Vector2(direction.x * boss.Stats.patrolSpeed, boss.Rb.linearVelocity.y);

        if ((direction.x > 0 && boss.transform.localScale.x < 0) ||
            (direction.x < 0 && boss.transform.localScale.x > 0))
        {
            movingToB = !movingToB;
            Flip();
        }
    }

    public void Exit()
    {
        boss.Rb.linearVelocity = Vector2.zero; 
    }

    private void Flip()
    {
        Vector3 scale = boss.transform.localScale;
        scale.x *= -1;
        boss.transform.localScale = scale;
    }
}
