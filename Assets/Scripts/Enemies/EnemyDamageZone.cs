using UnityEngine;

public class EnemyDamageZone : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO enemyStatsSO; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(enemyStatsSO.damage);
            }
        }
    }
}
