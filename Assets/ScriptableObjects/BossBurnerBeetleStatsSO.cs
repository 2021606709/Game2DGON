using UnityEngine;

[CreateAssetMenu(fileName = "BossStatsSO", menuName = "ScriptableObjects/BossStatsSO")]
public class BossBurnerBeeStatsSO : ScriptableObject
{
    [Header("General Stats")]
    public int maxHealth = 300;
    public int contactDamage = 10;

    [Header("Movement")]
    public float patrolSpeed = 2f;
    public float chargeSpeed = 8f;

    [Header("Cone Fire Attack")]
    public GameObject coneFirePrefab;
    public float coneFireCooldown = 3f;
    public float coneFireDuration = 1.5f;
    public int coneFireDamage = 10;

    [Header("Charge Attack")]
    public float chargeCooldown = 5f;
    public int chargeDamage = 15;

    [Header("Summon Minions")]
    public float summonCooldown = 2f;
    public GameObject minionPrefab;
    public int minionCount = 3;
}
