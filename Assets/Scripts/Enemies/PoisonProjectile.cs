using UnityEngine;

public class PoisonProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private Vector2 direction = Vector2.right;
    [SerializeField] private EnemyStatsSO enemyStatsSO;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        float actualSpeed = speed > 0 ? speed : enemyStatsSO.projectileSpeed;
        transform.Translate(direction * actualSpeed * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(enemyStatsSO.damage);
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
