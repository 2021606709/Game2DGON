using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private PlayerController playerController;
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        float moveSpeed = Mathf.Abs(playerController.GetHorizontalInput());
        animator.SetFloat("moveSpeed", moveSpeed);

        bool isRunning = moveSpeed > 0;
        bool isJumping = !playerController.IsGrounded();
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);   
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("isAttacking");
    }

    public void OnDeath()
    {
        animator.SetBool("isDead", true);
    }
}
