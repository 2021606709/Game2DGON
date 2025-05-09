using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO enemyStatsSO;
    private int currentHealth;
    private void Start()
    {
        currentHealth = enemyStatsSO.maxHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
