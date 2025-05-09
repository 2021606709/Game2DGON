using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool isActivated = false;
    private Animator checkPointAnimator;

    void Start()
    {
        checkPointAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if(playerHealth != null)
            {
                GameManager.Instance.SetCheckPoint(transform.position, playerHealth.CurrentHealth);
                checkPointAnimator.SetTrigger("Activate");
                isActivated = true;
            }
        }
    }
}
