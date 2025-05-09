using UnityEngine;

public class ScrapProjectile : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO enemyStatsSO;
    [SerializeField] private float lifeTime = 3f;
    
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(enemyStatsSO.damage);
            }
            Destroy(gameObject);
        }else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
