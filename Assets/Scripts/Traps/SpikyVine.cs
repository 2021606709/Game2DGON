using UnityEngine;

public class SpikyVine : MonoBehaviour
{
    [SerializeField] private GameObject hitBox;

    public void EnableHitBox()
    {
        if(hitBox != null)
        {
            hitBox.SetActive(true);
        }
    }

    public void DisableHitBox()
    {
        if(hitBox != null)
        {
            hitBox.SetActive(false);
        }
    }
}
