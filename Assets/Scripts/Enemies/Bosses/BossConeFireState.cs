using UnityEngine;

public class BossConeFireState : IBossState
{
    private BurnerBeetleAI boss;
    private float fireTimer = 0f; 
    private bool hasFired = false;

    public BossConeFireState(BurnerBeetleAI boss)
    {
        this.boss = boss;
    }

    public void Enter()
    {
        boss.FlipToward(boss.Player.position); 
        fireTimer = 0f; 
        hasFired = false;
        Debug.Log("Entering Cone Fire State");
        Debug.Log("Using prefab: " + boss.Stats.coneFirePrefab);
    }

    public void Update()
    {
        fireTimer += Time.deltaTime;
        if(!hasFired && fireTimer >= boss.Stats.coneFireCooldown) 
        {
            PerformConeFire();
            hasFired = true; 
        }
        if(fireTimer >= boss.Stats.coneFireCooldown + boss.Stats.coneFireDuration) 
        {
            boss.SwitchState(new BossIdleState(boss, 1f)); 
        }
    }

    public void FixedUpdate()
    {
        // Logic for firing projectiles in a cone pattern
        //FireConeProjectiles();
    }

    public void Exit()
    {
        // Logic to reset or clean up after the cone fire state
        Debug.Log("Exiting Cone Fire State");
    }

    private void PerformConeFire()
    {
        // Logic to fire projectiles in a cone pattern
        Debug.Log("Firing Cone Attack!");
        Vector2 fireDirection = (boss.Player.position - boss.transform.position).normalized;

        float[] angles = { -10f, 0f, 15f };
        foreach (float angle in angles)
        {
            Vector2 rotatedDirection = Quaternion.Euler(0, 0, angle) * fireDirection;
            GameObject fireball = Object.Instantiate(boss.Stats.coneFirePrefab, boss.transform.position, Quaternion.identity);
            Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = rotatedDirection * 4f;
            }
        }
    }
    
}
