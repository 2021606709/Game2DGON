using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStatsSO;
    private int currentHealth;
    private Animator animator; 
    private bool isDeath = false;
    private int lives = 3;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => playerStatsSO.maxHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = playerStatsSO.maxHealth;

        UIManager.Instance.UpdateHealth(currentHealth, MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeDamage(int amount)
    {
        if (isDeath) 
        {
            return;
        }

        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);

        UIManager.Instance.UpdateHealth(currentHealth, MaxHealth);

        if (currentHealth <= 0)
        {
            lives--;
            if(lives > 0)
            {
                Respawn();
            }
            else
            {
                Die();
            }

        }
    }
    public void Die()
    {
        if(isDeath) 
        {
            return;
        }
        isDeath = true;
        animator.SetBool("isDead", true);
        Invoke(nameof(TriggerGameOver), 2f);
    }
    private void TriggerGameOver()
    {
        GameManager gameManager = FindAnyObjectByType<GameManager>();
        if(gameManager != null){
            gameManager.GameOver();
        }
    }

    public void Respawn()
    {
        transform.position = GameManager.Instance.GetLastCheckPointPosition();
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        currentHealth = GameManager.Instance.GetCheckPointHealth();
        UIManager.Instance.UpdateHealth(currentHealth, MaxHealth);
        animator.Play("Idle");
    }
}
