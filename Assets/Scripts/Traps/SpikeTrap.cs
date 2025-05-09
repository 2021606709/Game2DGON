using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private TrapStatsSO trapStatsSO; 
    private bool isPlayerInside = false;
    private Coroutine damageCoroutine;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !isPlayerInside)
        {
            isPlayerInside = true;
            damageCoroutine = StartCoroutine(DealDamage(collision.GetComponent<PlayerHealth>()));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isPlayerInside = false;
            if(damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
            return;
        }
    }
    private System.Collections.IEnumerator DealDamage(PlayerHealth playerHealth)
    {
        while(true){
            if(playerHealth == null) yield break;

            playerHealth.TakeDamage(trapStatsSO.damage);
            yield return new WaitForSeconds(trapStatsSO.damageInterval);
        }
    }
}
