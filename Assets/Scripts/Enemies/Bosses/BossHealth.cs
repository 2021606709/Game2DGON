using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] private BossBurnerBeeStatsSO boss;
    [SerializeField] private GameObject portalToActive;
    private int currentHealth;

    private void Start()
    {
        currentHealth = boss.maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if(portalToActive != null)
        {
            portalToActive.SetActive(true);
        }
        Destroy(gameObject); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerProjectile")) // tag projectile của player
        {
            WaterProjectile projectile = collision.GetComponent<WaterProjectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.ProjectileStatsSO.damage); 
                Destroy(collision.gameObject);
            }
        }
    }
}
