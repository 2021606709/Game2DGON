using UnityEngine;

public class TrapDamageZone : MonoBehaviour
{
    [SerializeField] private TrapStatsSO trapStatsSO;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(trapStatsSO.damage);
            }
        }
    }
}
