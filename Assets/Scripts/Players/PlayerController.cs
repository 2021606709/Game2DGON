using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStatsSO;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private int maxJumpCount = 2;
    private bool isGrounded;
    private bool wasGrounded;
    private int currentJumpCount;
    private Rigidbody2D rb;
    private GameManager gameManager;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindAnyObjectByType<GameManager>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if(gameManager.IsGameOver()){
            return;
        }
        HandleMovement();
        HandleJump();
        if (transform.position.y < -30f)
        {
            GetComponent<PlayerHealth>().TakeDamage(playerStatsSO.maxHealth);
        }
    }
    private void HandleMovement(){
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * playerStatsSO.moveSpeed, rb.linearVelocity.y);
        
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void HandleJump(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if(isGrounded && !wasGrounded){
            currentJumpCount = maxJumpCount;
        }

        if(Input.GetButtonDown("Jump") && currentJumpCount > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, playerStatsSO.jumpForce);
            currentJumpCount--;
        }
        wasGrounded = isGrounded;
    }

    public float GetHorizontalInput(){
        return Input.GetAxis("Horizontal");
    }
    public bool IsGrounded(){
        return isGrounded;
    }
}
