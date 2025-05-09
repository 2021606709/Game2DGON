using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsSO", menuName = "ScriptableObjects/EnemyStatsSO")]
public class EnemyStatsSO : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    public float patrolRange = 3f;
    public float visionRange = 5f;

    [Header("Combat")]
    public int maxHealth = 50;
    public int damage = 15;

    [Header("Attack")]
    public float shootInterval = 2f;
    public float projectileSpeed = 4f;
    public GameObject enemyProjectilePrefab;
}
