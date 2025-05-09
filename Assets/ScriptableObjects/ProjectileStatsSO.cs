using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileStatsSO", menuName = "ScriptableObjects/ProjectileStatsSO")]
public class ProjectileStatsSO : ScriptableObject
{
    public float speed = 10f;
    public float lifeTime = 3f;
    public int damage = 10;
}
