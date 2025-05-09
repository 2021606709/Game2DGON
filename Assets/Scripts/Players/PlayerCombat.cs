using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private PlayerStatsSO playerStatsSO;
    [SerializeField] private Transform attackPoint;
    private float lastAttackTime;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J) && CanAttack()){
            Attack();
        }
    }

    bool CanAttack(){
        return Time.time - lastAttackTime > attackCooldown;
    }
    void Attack(){
        if(playerStatsSO.projecttilePrefab != null && attackPoint != null){
            GameObject projectile = Instantiate(playerStatsSO.projecttilePrefab, attackPoint.position, Quaternion.identity);
            projectile.transform.localScale = new Vector3(transform.localScale.x * Mathf.Abs(projectile.transform.localScale.x), 1, 1);
        }
        lastAttackTime = Time.time;
    }
}
