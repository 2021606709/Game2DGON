using UnityEngine;

public class BossSummonState : IBossState
{
    private BurnerBeetleAI boss;
    private int minionsToSummon;
    private float summonInterval;
    private float timeSinceLastSummon;
    private int summonedCount;

    public BossSummonState(BurnerBeetleAI boss)
    {
        boss.FlipToward(boss.Player.position);
        this.boss = boss;
        minionsToSummon = boss.Stats.minionCount;
        summonInterval = boss.Stats.summonCooldown;
        timeSinceLastSummon = 0f;
        summonedCount = 0;
    }

    public void Enter()
    {
        boss.Rb.linearVelocity = Vector2.zero; 
    }

    public void Update()
    {
        timeSinceLastSummon += Time.deltaTime;

        if (summonedCount < minionsToSummon && timeSinceLastSummon >= summonInterval)
        {
            SummonMinion();
            timeSinceLastSummon = 0f;
        }

        if (summonedCount >= minionsToSummon)
        {
            boss.SwitchState(new BossIdleState(boss, 1f)); 
        }
    }

    public void FixedUpdate() {}

    public void Exit() {}

    private void SummonMinion()
    {
        if (boss.Stats.minionPrefab != null)
        {
            Vector3 basePos = boss.transform.position + new Vector3(Random.Range(-1f, 1f), 1f, 0f);

            RaycastHit2D hit = Physics2D.Raycast(basePos, Vector2.down, 5f, LayerMask.GetMask("Ground"));
            Vector3 spawnPos = basePos;

            if (hit.collider != null)
            {
                spawnPos = hit.point + Vector2.up * 0.5f;
            }

            GameObject minion = GameObject.Instantiate(boss.Stats.minionPrefab, spawnPos, Quaternion.identity);
            summonedCount++;
        }
    }
}
