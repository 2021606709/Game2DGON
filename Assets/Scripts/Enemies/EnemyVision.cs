using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [Header("Enemy Vision Settings")]
    [SerializeField] private Transform visionOrigin;
    [SerializeField] private float visionDistance = 4f;
    [SerializeField] private LayerMask playerLayer;

    public GameObject DetectedPlayer { get; private set; }

    public bool IsPlayerInSight()
    {
        Collider2D hit = Physics2D.OverlapCircle(visionOrigin.position, visionDistance, playerLayer);
        DetectedPlayer = hit != null ? hit.gameObject : null;
        return DetectedPlayer != null;
    }

    private void OnDrawGizmosSelected()
    {
        if (visionOrigin != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(visionOrigin.position, visionDistance);
        }
    }
}
