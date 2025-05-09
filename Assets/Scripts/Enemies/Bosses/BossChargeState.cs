using UnityEngine;

public class BossChargeState : IBossState
{
    private BurnerBeetleAI boss;
    private Rigidbody2D rb;
    private float chargeSpeed;
    private float duration = 2f; 
    private bool hasFinished;
    private Vector2 chargeDirection;

    public BossChargeState(BurnerBeetleAI boss)
    {
        this.boss = boss;
        this.rb = boss.Rb;
        this.chargeSpeed = boss.Stats.chargeSpeed;
    }

    public void Enter()
    {
        boss.FlipToward(boss.Player.position);
        Vector2 direction = (boss.Player.position - boss.transform.position).normalized;
        chargeDirection = new Vector2(Mathf.Sign(direction.x), 0f);
        hasFinished = false;
    }

    public void Update()
    {
        Debug.Log($"Charge Update - elapsed: {boss.GetStateElapsedTime()}, hasFinished: {hasFinished}");
        if (!hasFinished && boss.GetStateElapsedTime() >= duration)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            hasFinished = true;

            boss.SwitchState(new BossIdleState(boss, 1f));
            Debug.Log("Charge Move: vel=" + rb.linearVelocity + ", dir=" + chargeDirection);
        }
    }

    public void FixedUpdate()
    {
        if (!hasFinished)
        {
            RaycastHit2D hit = Physics2D.Raycast(boss.transform.position, chargeDirection, 1f, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
                hasFinished = true;
                boss.SwitchState(new BossIdleState(boss, 1f));
                Debug.Log("Charge hit wall: " + hit.collider.name);
                return; 
            }
            Debug.DrawRay(boss.transform.position, chargeDirection * 1.5f, Color.red);
            rb.linearVelocity = new Vector2(chargeDirection.x * chargeSpeed, rb.linearVelocity.y);
        }
    }

    public void Exit()
    {
        rb.linearVelocity = Vector2.zero;
    }
}
