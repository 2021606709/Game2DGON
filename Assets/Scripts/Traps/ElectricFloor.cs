using System.Collections;
using UnityEngine;

public class ElectricFloor : MonoBehaviour
{
    [SerializeField] private Animator electricFloorAnimator;
    [SerializeField] private GameObject hitBox;
    [SerializeField] private TrapStatsSO trapStatsSO;
    [SerializeField] private float delayBeforeStart = 0f;
    

    void Start()
    {
        StartCoroutine(ElectricFloorLoop());
    }

    private IEnumerator ElectricFloorLoop()
    {
        yield return new WaitForSeconds(delayBeforeStart);
        while (true)
        {
            electricFloorAnimator.Play("ElectricFloor", 0, 0f);
            yield return new WaitForSeconds(electricFloorAnimator.GetCurrentAnimatorStateInfo(0).length);
            yield return new WaitForSeconds(trapStatsSO.damageInterval);
        }
    }

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
