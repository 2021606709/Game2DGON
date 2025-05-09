using Unity.VisualScripting;
using UnityEngine;

public class BossContactDamage : MonoBehaviour
{
    [SerializeField] private BossBurnerBeeStatsSO boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(boss.contactDamage);
            }
        }
    }
}
