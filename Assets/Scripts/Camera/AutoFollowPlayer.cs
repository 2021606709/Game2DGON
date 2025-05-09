using UnityEngine;
using Unity.Cinemachine;
public class AutoFollowPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if(player != null)
        {
            GetComponent<CinemachineCamera>().Follow = player.transform;
        }
    }
}
