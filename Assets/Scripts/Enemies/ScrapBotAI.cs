using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ScrapBotAI : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO enemyStatsSO;
    [SerializeField] private Transform firePoint;
    
    [Header("Override (Optional)")] 
    [SerializeField] private bool useOverride;
    [SerializeField] private float overrideSpeed;
    [SerializeField] private float overridePatrolRange;

    private Rigidbody2D rb;
    private EnemyVision enemyVision;
    private GameObject player;
    private Vector3 startPosition;
    private bool movingRight = true;
    private float nextFireTime;
    private float moveSpeed;
    private float patrolRange;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyVision = GetComponent<EnemyVision>();
        startPosition = transform.position;
        
        moveSpeed = useOverride ? overrideSpeed : enemyStatsSO.moveSpeed;
        patrolRange = useOverride ? overridePatrolRange : enemyStatsSO.patrolRange;
    }

    void FixedUpdate()
    {
        if(enemyVision != null && enemyVision.IsPlayerInSight())
        {
            player = enemyVision.DetectedPlayer;
            ChasePlayer();
            
            if(Time.time > nextFireTime)
            {
                FireTrippleShot();
                nextFireTime = Time.time + enemyStatsSO.shootInterval;
            }
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        float leftBound = startPosition.x - patrolRange;
        float rightBound = startPosition.x + patrolRange;

        float velocityX = (movingRight ? 1 : -1) * moveSpeed;
        rb.linearVelocity = new Vector2(velocityX, rb.linearVelocity.y);

        if((movingRight && transform.position.x >= rightBound) || 
           (!movingRight && transform.position.x <= leftBound))
        {
            Flip();
        }
    }

    private void ChasePlayer()
    {
        if(player == null)
        {
            return;
        }

        float direction = player.transform.position.x - transform.position.x;
        movingRight = direction > 0;
        float velocityX = (movingRight ? 1 : -1) * moveSpeed;
        rb.linearVelocity = new Vector2(velocityX, rb.linearVelocity.y);

        FlipIfNeeded();
    }

    private void FireTrippleShot()
    {
        if(enemyStatsSO.enemyProjectilePrefab == null || firePoint == null)
        {
            return;
        }

        Vector2 baseDirection = movingRight ? Vector2.right : Vector2.left;

        Vector2[] directions = new Vector2[]
        {
            baseDirection,
            (baseDirection + Vector2.up * 0.8f).normalized,
            (baseDirection + Vector2.up * 0.4f).normalized
        };

        foreach(Vector2 direction in directions)
        {
            GameObject projectile = Instantiate(enemyStatsSO.enemyProjectilePrefab, firePoint.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody2D>().linearVelocity = direction * enemyStatsSO.projectileSpeed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void Flip()
    {
        movingRight = !movingRight;
        FlipIfNeeded();
    }
    private void FlipIfNeeded()
    {
        Vector3 localScale = transform.localScale;
        if((movingRight && localScale.x < 0) || (!movingRight && localScale.x > 0))
        {
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
}
