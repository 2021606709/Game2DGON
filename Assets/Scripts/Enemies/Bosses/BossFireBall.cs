using UnityEngine;

public class BossFireBall : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(damage);

            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
