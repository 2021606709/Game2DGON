using UnityEngine;

public class WaterProjectile : MonoBehaviour
{
    [SerializeField] private ProjectileStatsSO projectileStatsSO;
    private Rigidbody2D rb;
    public ProjectileStatsSO ProjectileStatsSO => projectileStatsSO;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float direction = transform.localScale.x > 0 ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * projectileStatsSO.speed, rb.linearVelocity.y);
        Destroy(gameObject, projectileStatsSO.lifeTime);
    }

    void Update()
    {
        // Update logic if needed
        Debug.Log("Projectile Speed: " + rb.linearVelocity.magnitude);
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy")){
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if(enemyHealth != null){
                enemyHealth.TakeDamage(projectileStatsSO.damage);
            }
            Destroy(gameObject);
        }else if(collision.CompareTag("Ground")){
            Destroy(gameObject);
        }
    }
}
