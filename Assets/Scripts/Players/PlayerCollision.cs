using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.AddScore(1);
        }
        else if(collision.CompareTag("Portal"))
        {
            GameManager.Instance.Victory();
        }
    }
}
