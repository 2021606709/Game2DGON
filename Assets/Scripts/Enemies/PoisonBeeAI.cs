using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PoisonBeeAI : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO enemyStatsSO;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float patrolRange = 3f;

    private Rigidbody2D rb;
    private Vector3 startPosition;
    private bool movingRight = true;
    private float nextShootTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;   
    }

    void FixedUpdate()
    {
        Patrol();
        ShootInFourDirections();
    }

    private void Patrol()
    {
        float leftBound = startPosition.x - patrolRange;
        float rightBound = startPosition.x + patrolRange;
        float direction = movingRight ? 1 : -1;

        rb.linearVelocity = new Vector2(direction * enemyStatsSO.moveSpeed, rb.linearVelocity.y);

        if(transform.position.x >= rightBound && movingRight || transform.position.x <= leftBound && !movingRight)
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

    private void ShootInFourDirections()
    {
        if (Time.time >= nextShootTime && enemyStatsSO.enemyProjectilePrefab != null)
        {
            Vector2[] directrions = {
                Vector2.up,
                Vector2.right,
                Vector2.down,
                Vector2.left
            };

            foreach (Vector2 direction in directrions)
            {
                GameObject projectile = Instantiate(enemyStatsSO.enemyProjectilePrefab, shootPoint.position, Quaternion.identity);
                projectile.GetComponent<PoisonProjectile>().SetDirection(direction);
            }

            nextShootTime = Time.time + enemyStatsSO.shootInterval;
        }
    }
}
