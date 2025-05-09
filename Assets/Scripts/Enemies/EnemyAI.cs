using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO enemyStatsSO;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [Header("Override (Optional)")]
    [SerializeField] private bool useOverride;
    [SerializeField] private float overrideSpeed;
    [SerializeField] private float overridePatrolRange;

    private EnemyVision enemyVision;
    private GameObject player;
    private bool isChasing = false;
    private bool lostSight = false;

    private float speed;
    private float patrolRange;
    private Vector3 startPosition;
    private bool movingRight = true;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        if(useOverride)
        {
            speed = overrideSpeed;
            patrolRange = overridePatrolRange;
        }
        else
        {
            speed = enemyStatsSO.moveSpeed;
            patrolRange = enemyStatsSO.patrolRange;
        }
        enemyVision = GetComponent<EnemyVision>();
    }

    private void FixedUpdate()
    {
        DetectPlayer();

        if (isChasing)
        {
            ChasePlayer();
        }
        else if (lostSight)
        {
            ReturnToPatrol();
        }
        else
        {
            Patrol();
        }
    }
    private void DetectPlayer()
    {
        if (enemyVision.IsPlayerInSight())
        {
            player = enemyVision.DetectedPlayer;
            isChasing = true;
            lostSight = false;
        }
        else if (isChasing && player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if(distanceToPlayer > enemyStatsSO.visionRange)
            {
                lostSight = true;
                isChasing = false;
            }
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
        float velocityX = movingRight ? speed : -speed;
        rb.linearVelocity = new Vector2(velocityX, rb.linearVelocity.y);

        Vector3 scaler = transform.localScale;

        if((movingRight && scaler.x < 0) || (!movingRight && scaler.x > 0))
        {
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }
    private void ReturnToPatrol()
    {
        float distance = transform.position.x - startPosition.x;

        if(Mathf.Abs(distance) < enemyStatsSO.visionRange)
        {
            lostSight = false;
            return;
        }

        movingRight = distance < 0;
        float velocityX = movingRight ? speed : -speed;
        rb.linearVelocity = new Vector2(velocityX, rb.linearVelocity.y);

        Vector2 wallCheckDirection = movingRight ? Vector2.right : Vector2.left;
        bool hittingWall = Physics2D.Raycast(wallCheck.position, wallCheckDirection, wallCheckDistance, groundLayer);
        if(hittingWall)
        {
            Flip();
            return;
        }

        Vector3 scaler = transform.localScale;

        if((movingRight && scaler.x < 0) || (!movingRight && scaler.x > 0))
        {
            scaler.x *= -1;
            transform.localScale = scaler;
        }
    }
    private void Patrol()
    {
        float leftBound = startPosition.x - patrolRange;
        float rightBound = startPosition.x + patrolRange;

        Vector2 movement = (movingRight ? Vector2.right : Vector2.left) * speed;
        rb.linearVelocity = new Vector2(movement.x, rb.linearVelocity.y);

        Vector2 wallCheckDirection = movingRight ? Vector2.right : Vector2.left;
        bool hittingWall = Physics2D.Raycast(wallCheck.position, wallCheckDirection, wallCheckDistance, groundLayer);
        if (hittingWall || (movingRight && transform.position.x >= rightBound) 
            || (!movingRight && transform.position.x <= leftBound))
        {
            Flip();
        }
        // Debug.Log("Moving " + movingRight + " speed: " + speed);
        // Debug.Log("Velocity X: " + rb.linearVelocity.x);
    }
    private void Flip()
    {
        movingRight = !movingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
