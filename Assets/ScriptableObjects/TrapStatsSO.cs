using UnityEngine;

[CreateAssetMenu(fileName = "TrapStatsSO", menuName = "ScriptableObjects/TrapStatsSO")]
public class TrapStatsSO : ScriptableObject
{
    public int damage = 20;
    public bool instantKill = false; 
    public float damageInterval = 0.5f; 

}
