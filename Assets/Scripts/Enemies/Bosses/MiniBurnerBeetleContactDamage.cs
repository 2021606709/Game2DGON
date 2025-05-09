using UnityEngine;

public class MiniBurnerBeetleContactDamage : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO enemyStatsSO; 
    [SerializeField] private string playerTag = "Player";

    private bool hasDealtDamage = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasDealtDamage) return;

        if (other.CompareTag(playerTag))
        {
            Debug.Log("Minion hits player!");

            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(enemyStatsSO.damage);
                hasDealtDamage = true;
            }

            Destroy(gameObject); 
        }
    }
}
