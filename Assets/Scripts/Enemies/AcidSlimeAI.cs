using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AcidSlimeAI : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO enemyStatsSO;

    [Header("Override (Optional)")]
    [SerializeField] private bool useOverride;
    [SerializeField] private float overrideSpeed;
    [SerializeField] private float overridePatrolRange;

    private Rigidbody2D rb;
    private Vector3 startPosition;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;   
    }

    void FixedUpdate()
    {
        Patrol();
    }

    private void Patrol()
    {
        float patrolRange = useOverride ? overridePatrolRange : enemyStatsSO.patrolRange;
        float moveSpeed = useOverride ? overrideSpeed : enemyStatsSO.moveSpeed;

        float leftBound = startPosition.x - patrolRange;
        float rightBound = startPosition.x + patrolRange;

        float velocity = (movingRight ? 1 : -1) * moveSpeed;
        rb.MovePosition(rb.position + new Vector2(velocity * Time.fixedDeltaTime, 0));

        if ((movingRight && transform.position.x >= rightBound) || (!movingRight && transform.position.x <= leftBound))
        {
            Flip();
        }
    }

    private void Flip()
    {
        movingRight = !movingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
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
        }
    }
}
