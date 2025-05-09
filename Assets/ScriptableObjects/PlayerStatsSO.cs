using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStatsSO")]
public class PlayerStatsSO : ScriptableObject
{
    public int maxHealth = 100;
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    public float attackDamage = 10f;
    public GameObject projecttilePrefab;
}
