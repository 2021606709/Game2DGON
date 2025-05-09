using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ToxicDroneAI : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO enemyStatsSO;
    [SerializeField] private GameObject toxicProjectilePrefab;
    [SerializeField] private Transform shootPoint;

    private Rigidbody2D rb;
    private GameObject player;
    private EnemyVision enemyVision;
    private Vector3 startPosition;
    private bool movingRight = true;
    private float nextShootTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        enemyVision = GetComponent<EnemyVision>();
    }

    void FixedUpdate()
    {
        Patrol();
        DetectAndAttackPlayer();
    }

    private void Patrol()
    {
        float leftBound = startPosition.x - enemyStatsSO.patrolRange;
        float rightBound = startPosition.x + enemyStatsSO.patrolRange;

        float velocityX = (movingRight ? 1 : -1) * enemyStatsSO.moveSpeed;
        rb.linearVelocity = new Vector2(velocityX, rb.linearVelocity.y);

        if ((movingRight && transform.position.x >= rightBound) || (!movingRight && transform.position.x <= leftBound))
        {
            Flip();
        }
    }

    private void DetectAndAttackPlayer()
    {
        if(enemyVision == null || Time.time < nextShootTime)
        {
            return;
        }

        if(enemyVision.IsPlayerInSight())
        {
            player = enemyVision.DetectedPlayer;
            Vector2 direction = (player.transform.position - shootPoint.position).normalized;
            FireProjectile(direction);
            nextShootTime = Time.time + enemyStatsSO.shootInterval;
        }
    }

    private void FireProjectile(Vector2 direction)
    {
        if (toxicProjectilePrefab != null && shootPoint != null)
        {
            GameObject projectile = Instantiate(toxicProjectilePrefab, shootPoint.position, Quaternion.identity);

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * enemyStatsSO.projectileSpeed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    private void Flip()
    {
        movingRight = !movingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
