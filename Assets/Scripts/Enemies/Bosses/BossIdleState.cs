using UnityEngine;

public class BossIdleState : IBossState
{
    private BurnerBeetleAI boss;
    private float idleDuration = 1f;

    public BossIdleState(BurnerBeetleAI boss, float idleDuration)
    {
        this.boss = boss;
        this.idleDuration = idleDuration;
    }

    public void Enter()
    {
        Debug.Log("Entering Idle State");
    }

    public void Update()
    {
        if (boss.GetStateElapsedTime() >= idleDuration)
        {
            int random = Random.Range(0, 4);
            switch (random)
            {
                case 0:
                    boss.SwitchState(new BossConeFireState(boss));
                    Debug.Log("Switching to Cone Fire State");
                    break;
                case 1:
                    boss.SwitchState(new BossSummonState(boss));
                    Debug.Log("Switching to Summon State");
                    break;
                case 2:
                    boss.SwitchState(new BossChargeState(boss));
                    Debug.Log("Switching to Charge State");
                    break;
                case 3:
                    boss.SwitchState(new BossPatrolState(boss));
                    Debug.Log("Switching to Patrol State");
                    break;
            }
        }
    }

    public void FixedUpdate()
    {
    }

    public void Exit()
    {
        Debug.Log("Exiting Idle State");
    }
}
